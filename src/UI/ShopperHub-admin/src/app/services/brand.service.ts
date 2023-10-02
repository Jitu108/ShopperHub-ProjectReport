import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BrandRead } from '../models/brand-read';
import { BrandRepositoryService } from '../repositories/brand-repository.service';
import { BrandUpdate } from '../models/brand-update';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private repo: BrandRepositoryService) { }

  save(brand: BrandUpdate): Observable<boolean> {
    return this.repo.save(brand);
  }

  delete(brandId: number) {
    return this.repo.delete(brandId);
  }

  getAll(): Observable<BrandRead[]> {
    return this.repo.getAll();
  }

  getById(id: number): Observable<BrandRead> {
    return this.repo.getById(id);
  }
}
