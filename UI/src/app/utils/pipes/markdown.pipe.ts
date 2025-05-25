import { Pipe, PipeTransform, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { marked } from 'marked';

@Pipe({ name: 'markdown', standalone: false })
export class MarkdownPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(markdownText: string): SafeHtml {
    if (!markdownText) return '';
    let html = marked.parse(markdownText);
    html = this.sanitizer.sanitize(SecurityContext.HTML, html) || '';
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
}