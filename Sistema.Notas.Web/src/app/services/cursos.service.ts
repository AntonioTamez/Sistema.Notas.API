import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CursosService {

  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7040/api/Courses'; // URL de la API

  getCursos(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
