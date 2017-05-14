import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Headers, URLSearchParams, RequestMethod, RequestOptions, RequestOptionsArgs, Response, ResponseContentType } from '@angular/http';
import { Helpers } from '../core/services';
import { environment } from '../../environments/environment';
import { SocialInteractions } from './models';

@Injectable()
export class SocialInteractionsServiceApi {
  public defaultHeaders: Headers = new Headers();

  constructor(private http: Http) { }

  public updateSocialInteractions(socialInteractions: SocialInteractions, extraHttpRequestParams?: any): Observable<SocialInteractions> {
    return this.updateSocialInteractionsWithHttpInfo(socialInteractions, extraHttpRequestParams)
      .map((result: Response) => {
        if (result.status === 204) {
          return undefined;
        }
        else {
          return result.json();

        }
      })
  }

  public updateSocialInteractionsWithHttpInfo(socialInteraction: SocialInteractions, extraHttpRequestParams?: any): Observable<Response> {
    const path = environment.API_ENDPOINT + `/api/socialinteractions`;

    let queryParameters = new URLSearchParams();
    let headers = new Headers(this.defaultHeaders.toJSON());

    // to determine the Accept header
    let produces: string[] = [
      'application/json',
      'application/xml'
    ];


    headers.set('Authorization', 'Bearer ' + Helpers.getToken('id_token'));

    headers.set('Content-Type', 'application/json');

    let requestOptions: RequestOptionsArgs = new RequestOptions({
      method: RequestMethod.Put,
      headers: headers,
      body: socialInteraction === null ? '' : JSON.stringify(socialInteraction),
      search: queryParameters
    });

    // https://github.com/swagger-api/swagger-codegen/issues/4037
    if (extraHttpRequestParams) {
      requestOptions = (<any>Object).assign(requestOptions, extraHttpRequestParams);
    }

    return this.http.request(path, requestOptions);
  }

  public getSocialInteraction(extraHttpRequestParams?: any): Observable<SocialInteractions> {
    return this.getSocialInteractionWithHttpInfo(extraHttpRequestParams)
      .map((response: Response) => {
        if (response.status === 204) {
          return undefined;
        } else {
          return response.json();
        }
      });
  }

  public getSocialInteractionWithHttpInfo(postId?: string, extraHttpRequestParams?: any): Observable<Response> {
    const path = environment.API_ENDPOINT + `/api/socialinteraction`;

    let queryParameters = new URLSearchParams();
    if (postId) {
      queryParameters.set('postId', postId);
    }
    let headers = new Headers(this.defaultHeaders.toJSON());

    // to determine the Accept header
    let produces: string[] = [
      'application/json',
      'application/xml'
    ];


    headers.set('Authorization', 'Bearer ' + Helpers.getToken('id_token'));

    let requestOptions: RequestOptionsArgs = new RequestOptions({
      method: RequestMethod.Get,
      headers: headers,
      search: queryParameters
    });

    // https://github.com/swagger-api/swagger-codegen/issues/4037
    if (extraHttpRequestParams) {
      requestOptions = (<any>Object).assign(requestOptions, extraHttpRequestParams);
    }

    return this.http.request(path, requestOptions);
  }

}
