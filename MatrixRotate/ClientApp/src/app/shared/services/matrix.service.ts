import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MatrixService {

  constructor(private http: HttpClient) {

  }

  public generate(size: number): Observable<any> {
    const url = `${environment.apiUrl}matrixdata/generate`;
    return this.http.post(url, size)
      .pipe(
        catchError((error: any) => throwError(error))
      );
  }

  public getByRow(row: number, count?: number): Observable<number[][]> {
    let url = `${environment.apiUrl}matrixdata/${row}`;
    if (count) {
      url = `${url}/${count}`;
    }
    return this.http.get<number[][]>(url)
      .pipe(
        catchError((error: any) => throwError(error))
      );
  }

  public rotate(): Observable<any> {
    const url = `${environment.apiUrl}operations/rotate`;
    return this.http.post(url, '')
      .pipe(
        catchError((error: any) => throwError(error))
      );
  }

}
