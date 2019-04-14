import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { forEach } from '@angular/router/src/utils/collection';
import { BaseLookup } from '@app/entity/base-lookup';
import { MangaSource } from '@app/entity/manga/manga-source';
import { LookupService } from '@app/services/lookup.service';

@Component({
  selector: 'app-manga-page',
  templateUrl: './manga-page.component.html',
  styleUrls: ['./manga-page.component.css'],
  providers: []
})
export class MangaPageComponent implements OnInit, OnChanges{

  model: MangaInfo = new MangaInfo({});

  isChanged: boolean;

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
    var id = this.route.snapshot.paramMap.get('id');
    this.service.get(id)
      .subscribe(this.onAfterItemLoad.bind(this));
  }

  onAfterItemLoad(item) {
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
      this.lookupService.getAll().subscribe(items => this[lookupValuesColumn] = items);
      this[columnName + "Id"] = this.model[columnName].id;
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    this.isChanged = true;
  }

  constructor(
    private route: ActivatedRoute,
    private service: MangaSourcesService,
    private lookupService: LookupService,
    private location: Location
  ) { }

  onHrefPaste() {
  }

  goBack() {
    this.location.back();
  }

  save() {
    this.setupLookupValuesForSave();
    this.service.update(this.model).subscribe();
  }
}
