import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams, RequestMethod, RequestOptions, RequestOptionsArgs, Response, ResponseContentType } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { UserProfile } from './models';
import { Helpers } from '../core/services';
import { environment } from '../../environments/environment';

@Injectable()
export class ProfileServiceApi {

  public defaultHeaders: Headers = new Headers();

  constructor(protected http: Http) { }

  public updateProfile(extraHttpRequestParams?: any): Observable<UserProfile> {
    return this.updateProfileWithHttpInfo(extraHttpRequestParams)
      .map((response: Response) => {
        if (response.status === 204) {
          return undefined;
        } else {
          // return response.json();
        }
      });
  }

  public updateProfileWithHttpInfo(body: {},extraHttpRequestParams?: any): Observable<Response> {
    const path = environment.API_ENDPOINT + `/api/myprofile`;

    let headers = new Headers(this.defaultHeaders.toJSON());

    // to determine the Accept header
    let produces: string[] = [
      'application/json',
      'application/xml'
    ];


    headers.set('Authorization', 'Bearer ' + Helpers.getToken('id_token'));

    let requestOptions: RequestOptionsArgs = new RequestOptions({
      method: RequestMethod.Put,
      headers: headers,
      body: body
    });

    // https://github.com/swagger-api/swagger-codegen/issues/4037
    if (extraHttpRequestParams) {
      requestOptions = (<any>Object).assign(requestOptions, extraHttpRequestParams);
    }

    return this.http.request(path, requestOptions);
  }

  public getMyProfile(extraHttpRequestParams?: any): Observable<UserProfile> {
    return this.getMyProfileWithHttpInfo(extraHttpRequestParams)
      .map((response: Response) => {
        if (response.status === 204) {
          return undefined;
        } else {
          return response.json();
        }
      });
  }

  public getMyProfileWithHttpInfo(extraHttpRequestParams?: any): Observable<Response> {
    const path = environment.API_ENDPOINT + `/api/myprofile`;

    let headers = new Headers(this.defaultHeaders.toJSON());

    // to determine the Accept header
    let produces: string[] = [
      'application/json',
      'application/xml'
    ];


    headers.set('Authorization', 'Bearer ' + Helpers.getToken('id_token'));

    let requestOptions: RequestOptionsArgs = new RequestOptions({
      method: RequestMethod.Get,
      headers: headers
    });

    // https://github.com/swagger-api/swagger-codegen/issues/4037
    if (extraHttpRequestParams) {
      requestOptions = (<any>Object).assign(requestOptions, extraHttpRequestParams);
    }

    return this.http.request(path, requestOptions);
  }


  public uploadProfilePicture(file: File, extraHttpRequestParams?: any): Observable<Response> {

    const path = environment.API_ENDPOINT + `/api/myprofile`;

    let headers = new Headers(this.defaultHeaders.toJSON());

    // to determine the Accept header
    let produces: string[] = [
      'application/json',
      'application/xml'
    ];

    let formData = new FormData();
    formData.append('files', file);


    headers.set('Authorization', 'Bearer ' + Helpers.getToken('id_token'));

    let requestOptions: RequestOptionsArgs = new RequestOptions({
      method: RequestMethod.Post,
      headers: headers,
      body: formData
    });

    // https://github.com/swagger-api/swagger-codegen/issues/4037
    if (extraHttpRequestParams) {
      requestOptions = (<any>Object).assign(requestOptions, extraHttpRequestParams);
    }

    return this.http.request(path, requestOptions);

  }
}
