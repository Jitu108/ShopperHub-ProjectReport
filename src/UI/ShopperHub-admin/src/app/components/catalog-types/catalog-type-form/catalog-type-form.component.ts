import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TypeRead } from 'src/app/models/type-read';
import { TypeUpdate } from 'src/app/models/type-update';
import { TypeService } from 'src/app/services/type.service';

@Component({
  selector: 'app-catalog-type-form',
  templateUrl: './catalog-type-form.component.html',
  styleUrls: ['./catalog-type-form.component.scss']
})
export class CatalogTypeFormComponent implements OnInit {

  @ViewChild('nameInput', { read: ElementRef }) private nameInput: ElementRef;
  typeName: string;
  public mode: string;

  public typeId: number;
  public typeDetails$: Observable<TypeRead>;

  constructor(
    private activatedRoute: ActivatedRoute,
    private typeService: TypeService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(
      (params: Params) => {

        if (this.isNumeric(params['id'])) {
          this.typeId = parseInt(params['id']);
        }


        // Edit
        if (this.typeId !== undefined) {
          this.getTypeById(this.typeId);
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

  getTypeById(id: number) {
    this.typeDetails$ = this.typeService.getById(id);
  }

  onTypeSubmit(form: any) {
    var tId = this.typeId == undefined ? 0 : this.typeId;
    if (form.valid) {
      var type = {
        id: tId,
        type: this.nameInput.nativeElement.value
      } as TypeUpdate;

      this.typeService.save(type)
        .subscribe(x => {
          this.router.navigate(['/types'])
        });
    }
  }

  onCancelClick() {
    this.router.navigate(['/types']);
  }

}
