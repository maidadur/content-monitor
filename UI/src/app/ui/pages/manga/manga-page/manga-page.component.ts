import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MangaSourcesService } from '@app/services/manga/manga-sources.service';
import { MangaInfo } from '@app/entity/manga/manga-info';

@Component({
  selector: 'app-manga-page',
  templateUrl: './manga-page.component.html',
  styleUrls: ['./manga-page.component.css'],
  providers: []
})
export class MangaPageComponent implements OnInit, OnChanges{

  item: MangaInfo = new MangaInfo();

  isChanged: boolean;

  ngOnInit(): void {
    this.isChanged = false;
    var id = this.route.snapshot.paramMap.get('id');
    this.service.get(id)
      .subscribe(item => this.item = item);
  }

  ngOnChanges(changes: SimpleChanges) {
    this.isChanged = true;
  }

  constructor(
    private route: ActivatedRoute,
    private service: MangaSourcesService,
    private location: Location
  ) { }

  onHrefPaste() {
  }

  goBack() {
    this.location.back();
  }

  save() {
    this.service.update(this.item).subscribe();
  }
}
