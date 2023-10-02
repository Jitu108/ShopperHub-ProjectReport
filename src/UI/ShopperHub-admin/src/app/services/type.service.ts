import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TypeRead } from '../models/type-read';
import { TypeUpdate } from '../models/type-update';
import { TypeRepositoryService } from '../repositories/type-repository.service';

@Injectable({
  providedIn: 'root'
})
export class TypeService {

  constructor(private repo: TypeRepositoryService) { }

  save(type: TypeUpdate): Observable<boolean> {
    return this.repo.save(type);
  }

  delete(typeId: number) {
    return this.repo.delete(typeId);
  }

  getAll(): Observable<TypeRead[]> {
    return this.repo.getAll();
  }

  getById(id: number): Observable<TypeRead> {
    return this.repo.getById(id);
  }
}
