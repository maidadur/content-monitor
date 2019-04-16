import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { forEach } from '@angular/router/src/utils/collection';
import { BaseLookup } from '@app/entity/base-lookup';
import { MangaSource } from '@app/entity/manga/manga-source';
import { LookupService } from '@app/services/lookup.service';
import { CardMode } from '@app/entity/card-mode';
import { Guid } from "guid-typescript";

@Component({
  selector: 'app-manga-page',
  templateUrl: './manga-page.component.html',
  styleUrls: ['./manga-page.component.css'],
  providers: []
})
export class MangaPageComponent implements OnInit, OnChanges{

  model: MangaInfo = new MangaInfo({});

  isChanged: boolean;

  cardMode: CardMode;

  setupLookupValuesForSave() {
    for (let columnName in this.model.lookupColumns) {
      let lookupValuesColumn = columnName + "Values";
      if (!this[lookupValuesColumn]) {
        this[lookupValuesColumn] = [];
      }
      var lookupValue = this[columnName + "Id"];
      var item = this[lookupValuesColumn].find(item => item.id == lookupValue);
      this.model[columnName] = item || null;
    }
  }

  ngOnInit(): void {
    this.isChanged = false;
    this.loadData();
  }

  loadData(): void {
    var id = this.route.snapshot.paramMap.get('id');
    if (id === "new") {
      this.cardMode = CardMode.New;
      this.loadLookupValues();
    } else {
      this.service.get(id)
        .subscribe(this.onAfterItemLoad.bind(this));
    }
  }

  onAfterItemLoad(item) {
    if (item) {
      this.cardMode = CardMode.Edit;
    } else {
      this.cardMode = CardMode.New;
    }
    this.model = new MangaInfo(item);
    this.loadLookupValues();
  }

  loadLookupValues() {
    for (let columnName in this.model.lookupColumns) {
      let lookupSchema = this.model.lookupColumns[columnName];
      this.lookupService.schemaName = lookupSchema;
      let lookupValuesColumn = columnName + "Values";
      if (!this[lookupValuesColumn]) {
        this[lookupValuesColumn] = [];
      }
      this.lookupService.getAll().subscribe(items => {
        this[lookupValuesColumn] = items
      });
      this[columnName + "Id"] = this.model[columnName] && this.model[columnName].id;
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    this.isChanged = true;
  }

  constructor(
    private route: ActivatedRoute,
    private service: MangaSourcesService,
    private lookupService: LookupService,
    private location: Location,
    private router: Router
  ) { }

  onHrefPaste(event: any) {
    let clipboardData = event.clipboardData;
    this.model.href = clipboardData.getData('text');
    this.save(function () {
      this.service.loadMangaInfo(this.model.id).subscribe(() => {
        this.router.navigate(["/manga/sources", this.model.id]);
      });
    }.bind(this));
  }

  goBack() {
    this.location.back();
  }

  save(callback: Function) {
    callback = callback || function () { };
    this.setupLookupValuesForSave();
    if (this.cardMode == CardMode.New) {
      this.model.id = Guid.create().toJSON().value;
      this.service.add(this.model).subscribe(() => {
        this.cardMode = CardMode.Edit;
        callback()
      });
    } else {
      this.service.update(this.model).subscribe(() => {
        callback();
      });
    }
  }
}
