import { Routes } from '@angular/router';

import { MangaSectionComponent } from '../manga/manga-section/manga-section.component';
import { MangaPageComponent } from '../manga/manga-page/manga-page.component';
import { MangaTitlesSectionComponent } from '../manga/manga-titles-section/manga-titles-section.component';
import { MangaSourcesSectionComponent } from '../manga/manga-sources-section/manga-sources-section.component';
import { MangaSourcePageComponent } from '../manga/manga-source-page/manga-source-page.component';

export const workspaceRoutes: Routes = [
	{ path: 'manga', component: MangaSectionComponent },
	{ path: 'manga/titles', component: MangaTitlesSectionComponent },
	{ path: 'manga/sources', component: MangaSourcesSectionComponent },
	{ path: 'manga/titles/:id', component: MangaPageComponent },
	{ path: 'manga/sources/:id', component: MangaSourcePageComponent },
];
