import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BrandRead } from 'src/app/models/brand-read';
import { ProductCreate } from 'src/app/models/product-create';
import { ProductRead } from 'src/app/models/product-read';
import { TypeRead } from 'src/app/models/type-read';
import { BrandService } from 'src/app/services/brand.service';
import { ProductService } from 'src/app/services/product.service';
import { TypeService } from 'src/app/services/type.service';
import { DropDownModel } from '../../shared/drop-down-list/drop-down-list.component';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {

  public productId: number;
  public productDetail$: Observable<ProductRead>;
  public objProduct: ProductRead;
  public mode: string;

  public productToUpdate: ProductCreate = new ProductCreate();

  public type$: Observable<TypeRead[]>;
  public selectedTypeId: number;

  public brand$: Observable<BrandRead[]>;
  public selectedBrandId: number;

  public imageLabel = "Select Image";

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private typeService: TypeService,
    private brandService: BrandService
  ) { }

  ngOnInit(): void {
    this.loadData();
    this.activatedRoute.queryParams.subscribe(
      (params: Params) => {

        if (this.isNumeric(params['id'])) {
          this.productId = parseInt(params['id']);
        }


        // Edit
        if (this.productId !== undefined) {
          this.getProductById(this.productId);
          this.mode = "Edit";
        }
        // Add
        else {
          this.mode = "Add";
        }
      });
  }

  isNumeric =
    (value: string | number): boolean => {
      return (
        (value != null)
        && (value !== '')
        && !isNaN(Number(value.toString()))
      );
    }

  loadData() {
    this.loadTypes();
    this.loadBrand();
  }
  loadTypes() {
    // Get Types
    this.type$ = this.typeService.getAll();
  }

  loadBrand() {
    // Get Brands
    this.brand$ = this.brandService.getAll();
  }

  getProductById(id: number) {
    this.productDetail$ = this.productService.getById(id);
    this.productDetail$.subscribe(x => {
      this.selectedTypeId = x.catalogTypeId;
      this.selectedBrandId = x.catalogBrandId;

      this.productToUpdate = this.productService.getProductCreateByReadObject(x);
      this.imageLabel = this.productToUpdate.imageName;
    });

  }
  onNameChange(event) {
    this.productToUpdate.name = event.target.value;
  }

  onDescChange(event) {
    this.productToUpdate.description = event.target.value;
  }

  onPriceChange(event) {
    this.productToUpdate.price = event.target.value;
  }

  onMRPChange(event) {
    this.productToUpdate.mrp = event.target.value;
  }

  onAvailableChange(event) {
    this.productToUpdate.availableStock = event.target.value;
  }

  onRestockThresholdChange(event) {
    this.productToUpdate.restockThreshold = event.target.value;
  }

  onMaxThresholdChange(event) {
    this.productToUpdate.maxStockThreshold = event.target.value;
  }



  onTypeSelectionChange(selectedType) {
    this.productToUpdate.catalogTypeId = selectedType === null ? 0 : parseInt(selectedType.value);

  }

  onBrandSelectionChange(selectedBrand) {
    this.productToUpdate.catalogBrandId = selectedBrand === null ? 0 : parseInt(selectedBrand.value);
  }

  onImageCaptionChange(event) {
    this.productToUpdate.imageCaption = event.target.value;
  }

  onFileSelected(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]);

      reader.onload = (evnt) => {
        this.productToUpdate.imageData = evnt.target.result;
        this.productToUpdate.imageName = event.target.files[0].name;
        this.imageLabel = this.productToUpdate.imageName;
      }
    }
  }



  onProductSubmit(form) {
    if (form.valid) {
      if (this.productToUpdate.price !== null && this.productToUpdate.price.toString().trim() === "") this.productToUpdate.price = null;

      this.productService.save(this.productToUpdate)
        .subscribe(x => {
          this.router.navigate(['/products']);
        });

    }
  }

  onCancelClick() {
    this.router.navigate(['/products']);
  }

}
