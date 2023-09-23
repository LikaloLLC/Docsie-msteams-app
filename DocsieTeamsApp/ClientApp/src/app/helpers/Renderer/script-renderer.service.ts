import { ElementRef, Injectable, Renderer2 } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ScriptRendererService {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  renderScript(scriptUrl: string, customAttributes: { [key: string]: string }) {
    // Create a <script> element
    const script = this.renderer.createElement('script');

    // Set the script source URL
    this.renderer.setAttribute(script, 'src', scriptUrl);

    // Add custom attributes to the script element
    for (const key in customAttributes) {
      if (customAttributes.hasOwnProperty(key)) {
        this.renderer.setAttribute(script, key, customAttributes[key]);
      }
    }

    // Append the script element to the component's native element
    this.renderer.appendChild(this.el.nativeElement, script);
  }
}
