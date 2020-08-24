import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material';

import { AppComponent } from './app.component';
import { HomeComponent } from './ui/pages/home/home.component';
import { NavMenuComponent } from './ui/controls/nav-menu/nav-menu.component';
import { MangaSectionComponent } from './ui/pages/manga/manga-section/manga-section.component';
import { MangaPageComponent } from './ui/pages/manga/manga-page/manga-page.component';
import { MangaTitlesSectionComponent } from './ui/pages/manga/manga-titles-section/manga-titles-section.component';
import { DialogWindowComponent } from './ui/controls/dialog-window/dialog-window.component';
import { BaseDetailComponent } from './ui/controls/base-detail/base-detail.component';
import { MangaChaptersDetailComponent } from './ui/controls/manga/manga-chapters-detail/manga-chapters-detail.component';
import { MangaSourcesSectionComponent } from './ui/pages/manga/manga-sources-section/manga-sources-section.component';
import { MangaSourcePageComponent } from './ui/pages/manga/manga-source-page/manga-source-page.component';
import { ViewPortComponent } from './ui/controls/view-port/view-port.component';

@NgModule({
  declarations: [  
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    MangaSectionComponent,
    MangaPageComponent,
    MangaTitlesSectionComponent,
    DialogWindowComponent,
    BaseDetailComponent,
    MangaChaptersDetailComponent,
    MangaSourcesSectionComponent,
    MangaSourcePageComponent,
    ViewPortComponent,
  ],
  entryComponents: [DialogWindowComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'manga', component: MangaSectionComponent },
      { path: 'manga/titles', component: MangaTitlesSectionComponent },
      { path: 'manga/sources', component: MangaSourcesSectionComponent },
      { path: 'manga/titles/:id', component: MangaPageComponent },
      { path: 'manga/sources/:id', component: MangaSourcePageComponent },
    ]),
    BrowserAnimationsModule,
    MatGridListModule,
    MatListModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDialogModule,
    NgbModule.forRoot() 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
