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
import { MatDialogModule } from '@angular/material/dialog';

import { HomeComponent } from '../home/home.component';
import { NavMenuComponent } from '../../controls//nav-menu/nav-menu.component';
import { MangaSectionComponent } from '..//manga/manga-section/manga-section.component';
import { MangaPageComponent } from '..//manga/manga-page/manga-page.component';
import { MangaTitlesSectionComponent } from '..//manga/manga-titles-section/manga-titles-section.component';
import { DialogWindowComponent } from '../../controls//dialog-window/dialog-window.component';
import { BaseDetailComponent } from '../../controls//base-detail/base-detail.component';
import { MangaChaptersDetailComponent } from '../../controls//manga/manga-chapters-detail/manga-chapters-detail.component';
import { MangaSourcesSectionComponent } from '..//manga/manga-sources-section/manga-sources-section.component';
import { MangaSourcePageComponent } from '..//manga/manga-source-page/manga-source-page.component';
import { ViewPortComponent } from '../../controls//view-port/view-port.component';
import { WorkspaceComponent } from './workspace.component';

@NgModule({
	declarations: [
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
		WorkspaceComponent,
	],
	entryComponents: [DialogWindowComponent],
	imports: [
		BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
		HttpClientModule,
		FormsModule,
		RouterModule,
		BrowserAnimationsModule,
		MatGridListModule,
		MatListModule,
		MatIconModule,
		MatFormFieldModule,
		MatInputModule,
		MatSelectModule,
		MatButtonModule,
		MatDialogModule,
		NgbModule,
	],
})
export class WorkspaceModule {}
