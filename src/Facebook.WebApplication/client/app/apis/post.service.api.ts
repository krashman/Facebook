import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams, RequestMethod, RequestOptions, RequestOptionsArgs, Response, ResponseContentType } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Post } from './models';
import { Helpers } from '../core/services';
import { environment } from '../../environments/environment';
@Injectable()
export class PostServiceApi {
  public defaultHeaders: Headers = new Headers();

  constructor(protected http: Http) { }

  public addPost(body: Post, extraHttpRequestParams?: any): Observable<{}> {
    return this.addPostWithHttpInfo(body, extraHttpRequestParams)
      .map((response: Response) => {
        if (response.status === 204) {
          return undefined;
        } else {
          return response.json();
        }
      });
  }

  public addPostWithHttpInfo(body?: Post, extraHttpRequestParams?: any): Observable<Response> {
    const path = environment.API_ENDPOINT + `/api/posts`;

    let queryParameters = new URLSearchParams();
    let headers = new Headers(this.defaultHeaders.toJSON());
    let consumes: string[] = [
      'application/json',
      'application/xml'
    ];

    // to determine the Accept header
    let produces: string[] = [
      'application/json',
      'application/xml'
    ];


    headers.set('Authorization', 'Bearer ' + Helpers.getToken('id_token'));

    headers.set('Content-Type', 'application/json');

    let requestOptions: RequestOptionsArgs = new RequestOptions({
      method: RequestMethod.Post,
      headers: headers,
      body: body == null ? '' : JSON.stringify(body),
      search: queryParameters
    });

    if (extraHttpRequestParams) {
      requestOptions = (<any>Object).assign(requestOptions, extraHttpRequestParams);
    }

    return this.http.request(path, requestOptions);
  }

}
