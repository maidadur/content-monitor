import { Component, OnInit } from '@angular/core';
import { MangaChapterNotificationService } from '@app/services/manga/manga-chapter-notification.service';
import { MangaChapterNotification } from '@app/entity/manga/manga-chapter-notification';

@Component({
  selector: 'app-manga-section',
  templateUrl: './manga-section.component.html',
  styleUrls: ['./manga-section.component.css']
})
export class MangaSectionComponent implements OnInit {

  constructor(private service: MangaChapterNotificationService) { }

  items: MangaChapterNotification[];

  ngOnInit() {
    this.service.getAllNotifications()
      .subscribe(items => this.items = items); 
  }

}
