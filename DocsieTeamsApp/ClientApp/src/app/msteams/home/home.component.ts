import { Component, ElementRef, Renderer2 } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  deploymentScript: string | null;

  constructor(private el: ElementRef, private renderer: Renderer2, private route: ActivatedRoute) { }


  ngOnInit() {

    this.deploymentScript = this.route.snapshot.queryParamMap.get('script');

    if (this.deploymentScript) {
      let srcAttribute = this.extractScriptSrcFromHtml(this.deploymentScript);
      let data_docsie = this.extractDataDocsieAttributeFromHtml(this.deploymentScript);

      if (srcAttribute && data_docsie) {
        const customAttributes = {
          'data-docsie': data_docsie
        };
        this.renderScript(srcAttribute, customAttributes);
      } else {
        console.log("Something wrong");
      }
    }
  }




  renderScript(scriptUrl: string, customAttributes: { [key: string]: string }) {
    const script = this.renderer.createElement('script');
    this.renderer.setAttribute(script, 'src', scriptUrl);
    for (const key in customAttributes) {
      if (customAttributes.hasOwnProperty(key)) {
        this.renderer.setAttribute(script, key, customAttributes[key]);
      }
    }
    this.renderer.appendChild(this.el.nativeElement, script);
  }

  extractScriptSrcFromHtml(htmlString: string): string | null {
    const scriptRegex = /<script.*?src=["'](.*?)["'].*?<\/script>/i;
    const match = scriptRegex.exec(htmlString);
    return match ? match[1] : null;
  }

  extractDataDocsieAttributeFromHtml(htmlString: string): string | null {
    const scriptRegex = /<script.*?data-docsie=["'](.*?)["'].*?<\/script>/i;
    const match = scriptRegex.exec(htmlString);
    return match ? match[1] : null;
  }
}
