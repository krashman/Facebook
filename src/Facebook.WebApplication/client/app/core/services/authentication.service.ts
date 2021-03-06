import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subscription } from 'rxjs/Subscription';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { environment } from '../../../environments/environment';
import { AuthHttp } from 'angular2-jwt';
import { User } from '../models/index';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {
    redirectUrl: string;

    constructor(private http: Http, private authHttp: AuthHttp, private router: Router) {        // On bootstrap or refresh, tries to get users'data.
        this.getUserInfo();
        this.headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        this.options = new RequestOptions({ headers: this.headers });
    }


    public getUser(): Observable<any> {
        return this.userSubject.asObservable();
    }


    /**
     * Behavior subjects of the user's status, data & roles.
     * https://netbasal.com/angular-2-persist-your-login-status-with-behaviorsubject-45da9ec43243#.14rltx9dh
     */
    public signinSubject = new BehaviorSubject<boolean>(this.tokenNotExpired());


    public userSubject = new BehaviorSubject<any>({});

    public rolesSubject = new BehaviorSubject<string[]>([]);
    /**
     * Offset for the scheduling to avoid the inconsistency of data on the client.
     */
    private offsetSeconds: number = 30;

    refreshSubscription: Subscription;


    /**
     * Token info.
     */
    private expiresIn: number;
    private authTime: number;

    private headers: Headers;
    private options: RequestOptions;


    //TODO: change to query-string
    private encodeParams(params: any): string {
        let body: string = "";
        for (let key in params) {
            if (body.length) {
                body += "&";
            }
            body += key + "=";
            body += encodeURIComponent(params[key]);
        }
        return body;
    }

    /**
     * Checks if user is signed in.
     */
    public isSignedIn(): Observable<boolean> {
        return this.signinSubject.asObservable();
    }

    public signOut(): void {
        this.redirectUrl = null;

        this.signinSubject.next(false);
        this.userSubject.next({});
        this.rolesSubject.next([]);

        this.unscheduleRefresh();

        this.revokeToken();
        this.revokeRefreshToken();
    }

    public unscheduleRefresh(): void {
        if (this.refreshSubscription) {
            this.refreshSubscription.unsubscribe();
        }
    }

    /**
     * Case when the user comes back to the app after closing it.
     */
    public startupTokenRefresh(): void {
        // If the user is authenticated, uses the token stream
        // provided by angular2-jwt and flatMap the token.
        if (this.signinSubject.getValue()) {
            let source = this.authHttp.tokenStream.flatMap(
                (token: string) => {
                    let now: number = new Date().valueOf();
                    let exp: number = Helpers.getExp();
                    let delay: number = exp - now - this.offsetSeconds * 1000;

                    // Uses the delay in a timer to run the refresh at the proper time.
                    return Observable.timer(delay);
                });

            // Once the delay time from above is reached, gets a new JWT and schedules additional refreshes.
            source.subscribe(() => {
                this.getNewToken().subscribe(
                    () => {
                        this.scheduleRefresh();
                    },
                    (error: any) => {
                        // Need to handle this error.
                        console.log(error);
                    }
                );
            });
        }
    }

    private revokeToken() {
        Helpers.removeToken('id_token');
        Helpers.removeExp();

    }

    public revokeRefreshToken(): void {
        let refreshToken: string = Helpers.getToken('refresh_token');

        if (refreshToken) {
            let revocationEndpoint: string = environment.REVOCATION_ENDPOINT;

            let params: any = {
                client_id: environment.CLIENT_ID,
                token_type_hint: "refresh_token",
                token: refreshToken
            };

            let body: string = this.encodeParams(params);

            this.http.post(revocationEndpoint, body, this.options)
                .subscribe(() => {
                    Helpers.removeToken('refresh_token');
                })
        }
    }

    public signIn(username: string, password: string): Observable<any> {
        let tokenEndpoint: string = environment.TOKEN_ENDPOINT;

        let params: any = {
            client_id: environment.CLIENT_ID,
            grant_type: environment.GRANT_TYPE,
            username: username,
            password: password,
            scope: environment.SCOPE
        };

        let body: string = this.encodeParams(params);

        this.authTime = new Date().valueOf();

        return this.http.post(tokenEndpoint, body, this.options)
            .map((res: Response) => {
                let body: any = res.json();
                if (typeof body.access_token !== 'undefined') {
                    // Stores access token & refresh token.
                    this.store(body);
                    this.getUserInfo();

                    // Tells all the subscribers about the new status.
                    this.signinSubject.next(true);

                    // Optional strategy for refresh token through a scheduler.
                    this.scheduleRefresh();
                }
            }).catch((error: any) => {
                // Checks for error in response (error from the Token endpoint).
                if (error.body != "") {
                    let body: any = error.json();

                    switch (body.error) {
                        case "invalid_grant":
                            error = "Invalid email or password";
                            break;
                        default:
                            error = "Unexpected error. Try again";
                    }
                } else {
                    let errMsg = (error.message) ? error.message :
                        error.status ? `${error.status} - ${error.statusText}` : 'Server error';
                    console.log(errMsg);
                    error = "Server error. Try later.";
                }
                return Observable.throw(error)
            });
    }

    /**
     * Optional strategy for refresh token through a scheduler.
     * Will schedule a refresh at the appropriate time.
     */
    public scheduleRefresh(): void {
        let source = this.authHttp.tokenStream.flatMap(
            (token: string) => {
                let delay: number = this.expiresIn - this.offsetSeconds * 1000;
                return Observable.interval(delay);
            });

        this.refreshSubscription = source.subscribe(() => {
            this.getNewToken().subscribe(
                () => {
                    // Scheduler works.
                },
                (error: any) => {
                    // Need to handle this error.
                    console.log(error);
                }
            );
        });
    }


    public getNewToken(): Observable<any> {
        let refreshToken: string = Helpers.getToken('refresh_token');

        let tokenEndpoint: string = environment.TOKEN_ENDPOINT;

        let params: any = {
            client_id: environment.CLIENT_ID,
            grant_type: "refresh_token",
            refresh_token: refreshToken
        };

        let body: string = this.encodeParams(params);

        this.authTime = new Date().valueOf();

        return this.http.post(tokenEndpoint, body, this.options)
            .map((res: Response) => {
                let body: any = res.json();
                if (typeof body.access_token !== 'undefined') {
                    // Stores access token & refresh token.
                    this.store(body);
                }
            }).catch((error: any) => {
                return Observable.throw(error);
            });
    }

    /**
     * Checks for presence of token and that token hasn't expired.
     */
    private tokenNotExpired(): boolean {
        let token: string = Helpers.getToken('id_token');
        return token != null && (Helpers.getExp() > new Date().valueOf());
    }

    /**
     * Calls UserInfo endpoint to retrieve user's data.
     */
    public getUserInfo(): void {
        if (this.tokenNotExpired()) {
            this.authHttp.get(environment.USERINFO_ENDPOINT)
                .subscribe(
                (res: any) => {
                    let user: any = res.json();
                    let roles: string[] = user.role;
                    // Tells all the subscribers about the new data & roles.
                    this.userSubject.next(user);
                    this.rolesSubject.next(user.role);
                },
                (error: any) => {
                    this.signinSubject.next(false);
                    this.router.navigate(['/login']);
                    console.log(error);
                });
        }
    }

    /**
     * Stores access token & refresh token.
     */
    private store(body: any): void {
        Helpers.setToken('id_token', body.access_token);
        Helpers.setToken('refresh_token', body.refresh_token);

        // Calculates token expiration.
        this.expiresIn = <number>body.expires_in * 1000; // To milliseconds.
        Helpers.setExp(this.authTime + this.expiresIn);
    }


    getAll() {
        return this.http.get(environment.API_ENDPOINT + '/api/users', this.jwt()).map((response: Response) => response.json());
    }

    getById(id: number) {
        return this.http.get(environment.API_ENDPOINT + '/api/users/' + id, this.jwt()).map((response: Response) => {
            response.json();
        });
    }

    create(user: User) {
        return this.http.post(environment.API_ENDPOINT + '/api/users', user).map((response: Response) => response.json());
    }

    update(user: User) {
        return this.http.put(environment.API_ENDPOINT + '/api/users/' + user.id, user, this.jwt()).map((response: Response) => response.json());
    }

    delete(id: number) {
        return this.http.delete(environment.API_ENDPOINT + '/api/users/' + id, this.jwt()).map((response: Response) => response.json());
    }

    // private helper methods

    private jwt() {
        // create authorization header with jwt token
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
}



// Set Helpers to use the same storage in AppModule.
export class Helpers {

    public static getToken(name: string): string {
        return localStorage.getItem(name);
    }

    public static setToken(name: string, value: string) {
        localStorage.setItem(name, value);
    }

    public static removeToken(name: string): void {
        localStorage.removeItem(name);
    }

    public static setExp(exp: number) {
        localStorage.setItem("exp", exp.toString());
    }

    /**
     * Returns token expiration in milliseconds.
     */
    public static getExp(): number {
        return parseInt(localStorage.getItem("exp"));
    }

    public static removeExp(): void {
        localStorage.removeItem("exp");
    }

}
