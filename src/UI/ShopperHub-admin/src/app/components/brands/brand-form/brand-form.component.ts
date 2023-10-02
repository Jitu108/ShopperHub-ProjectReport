import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { BrandRead } from 'src/app/models/brand-read';
import { BrandUpdate } from 'src/app/models/brand-update';
import { BrandService } from 'src/app/services/brand.service';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.scss']
})
export class BrandFormComponent implements OnInit {

  @ViewChild('nameInput', { read: ElementRef }) private nameInput: ElementRef;
  brandName: string;
  public mode: string;

  public brandId: number;
  public brandDetails$: Observable<BrandRead>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private brandService: BrandService,
    private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(
      (params: Params) => {
        if (this.isNumeric(params['id'])) {
          this.brandId = parseInt(params['id']);
        }

        //Edit
        if (this.brandId !== undefined) {
          this.getBrandById(this.brandId);
          this.mode = "Edit";
        }

        // Add
        else {
          this.mode = "Add";
        }
      }
    )
  }

  isNumeric =
    (value: string | number): boolean => {
      return (
        (value != null)
        && (value !== '')
        && !isNaN(Number(value.toString()))
      );
    }

  getBrandById(id: number) {
    this.brandDetails$ = this.brandService.getById(id);
  }

  onBrandSubmit(form: any) {
    var brId = this.brandId == undefined ? 0 : this.brandId;
    if (form.valid) {
      var brand = {
        id: brId,
        brand: this.nameInput.nativeElement.value
      } as BrandUpdate;

      this.brandService.save(brand)
        .subscribe(x => {
          this.router.navigate(['/brands'])
        });
    }
  }

  onCancelClick() {
    this.router.navigate(['/brands'])
  }

}
