import {
  DomSanitizer
} from "./chunk-UCVLFZR6.js";
import "./chunk-GZUGOFDH.js";
import {
  CommonModule,
  NgClass,
  NgForOf,
  NgIf
} from "./chunk-QKMHPHWO.js";
import {
  Component,
  EventEmitter,
  Injectable,
  Input,
  NgModule,
  Output,
  Pipe,
  setClassMetadata,
  ɵɵadvance,
  ɵɵdefineComponent,
  ɵɵdefineInjectable,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵdefinePipe,
  ɵɵdirectiveInject,
  ɵɵelement,
  ɵɵelementEnd,
  ɵɵelementStart,
  ɵɵgetCurrentView,
  ɵɵinject,
  ɵɵlistener,
  ɵɵnextContext,
  ɵɵpipe,
  ɵɵpipeBind1,
  ɵɵproperty,
  ɵɵpureFunction1,
  ɵɵpureFunction2,
  ɵɵresetView,
  ɵɵrestoreView,
  ɵɵsanitizeHtml,
  ɵɵstyleProp,
  ɵɵtemplate
} from "./chunk-EHYBSXCX.js";
import "./chunk-Q3WJNBSC.js";
import "./chunk-5XGFKBFE.js";
import {
  Subject
} from "./chunk-2QJ2XPTW.js";

// node_modules/ngx-toasta/fesm2020/ngx-toasta.mjs
function ToastComponent_div_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r4 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 4);
    ɵɵlistener("click", function ToastComponent_div_1_Template_div_click_0_listener($event) {
      ɵɵrestoreView(_r4);
      const ctx_r3 = ɵɵnextContext();
      return ɵɵresetView(ctx_r3.close($event));
    });
    ɵɵelementEnd();
  }
}
function ToastComponent_div_2_span_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "span", 9);
    ɵɵpipe(1, "safeHtml");
  }
  if (rf & 2) {
    const ctx_r5 = ɵɵnextContext(2);
    ɵɵproperty("innerHTML", ɵɵpipeBind1(1, 1, ctx_r5.toast.title), ɵɵsanitizeHtml);
  }
}
function ToastComponent_div_2_br_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "br");
  }
}
function ToastComponent_div_2_span_3_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "span", 10);
    ɵɵpipe(1, "safeHtml");
  }
  if (rf & 2) {
    const ctx_r7 = ɵɵnextContext(2);
    ɵɵproperty("innerHtml", ɵɵpipeBind1(1, 1, ctx_r7.toast.msg), ɵɵsanitizeHtml);
  }
}
function ToastComponent_div_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 5);
    ɵɵtemplate(1, ToastComponent_div_2_span_1_Template, 2, 3, "span", 6)(2, ToastComponent_div_2_br_2_Template, 1, 0, "br", 7)(3, ToastComponent_div_2_span_3_Template, 2, 3, "span", 8);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵproperty("ngIf", ctx_r1.toast.title);
    ɵɵadvance();
    ɵɵproperty("ngIf", ctx_r1.toast.title && ctx_r1.toast.msg);
    ɵɵadvance();
    ɵɵproperty("ngIf", ctx_r1.toast.msg);
  }
}
function ToastComponent_div_3_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 11);
    ɵɵelement(1, "div", 12);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵstyleProp("width", ctx_r2.progressPercent, "%");
  }
}
var _c0 = (a0, a1) => [a0, a1];
function ToastaComponent_ngx_toast_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r3 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "ngx-toast", 2);
    ɵɵlistener("closeToast", function ToastaComponent_ngx_toast_1_Template_ngx_toast_closeToast_0_listener() {
      const restoredCtx = ɵɵrestoreView(_r3);
      const toast_r1 = restoredCtx.$implicit;
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.closeToast(toast_r1));
    });
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const toast_r1 = ctx.$implicit;
    ɵɵproperty("toast", toast_r1);
  }
}
var _c1 = (a0) => [a0];
function isString(obj) {
  return typeof obj === "string";
}
function isNumber(obj) {
  return typeof obj === "number";
}
function isFunction(obj) {
  return typeof obj === "function";
}
var ToastOptions = class {
};
ToastOptions.ɵfac = function ToastOptions_Factory(t) {
  return new (t || ToastOptions)();
};
ToastOptions.ɵprov = ɵɵdefineInjectable({
  token: ToastOptions,
  factory: ToastOptions.ɵfac
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastOptions, [{
    type: Injectable
  }], null, null);
})();
var ToastData = class {
};
ToastData.ɵfac = function ToastData_Factory(t) {
  return new (t || ToastData)();
};
ToastData.ɵprov = ɵɵdefineInjectable({
  token: ToastData,
  factory: ToastData.ɵfac
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastData, [{
    type: Injectable
  }], null, null);
})();
var ToastaConfig = class {
  constructor() {
    this.limit = 5;
    this.showClose = true;
    this.showDuration = true;
    this.position = "bottom-right";
    this.timeout = 5e3;
    this.theme = "default";
  }
};
ToastaConfig.ɵfac = function ToastaConfig_Factory(t) {
  return new (t || ToastaConfig)();
};
ToastaConfig.ɵprov = ɵɵdefineInjectable({
  token: ToastaConfig,
  factory: ToastaConfig.ɵfac
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastaConfig, [{
    type: Injectable
  }], null, null);
})();
var ToastaEventType;
(function(ToastaEventType2) {
  ToastaEventType2[ToastaEventType2["ADD"] = 0] = "ADD";
  ToastaEventType2[ToastaEventType2["CLEAR"] = 1] = "CLEAR";
  ToastaEventType2[ToastaEventType2["CLEAR_ALL"] = 2] = "CLEAR_ALL";
})(ToastaEventType || (ToastaEventType = {}));
var ToastaEvent = class {
  constructor(type, value) {
    this.type = type;
    this.value = value;
  }
};
function toastaServiceFactory(config) {
  return new ToastaService(config);
}
var ToastaService = class _ToastaService {
  constructor(config) {
    this.config = config;
    this.uniqueCounter = 0;
    this.eventSource = new Subject();
    this.events = this.eventSource.asObservable();
  }
  /**
   * Get list of toats
   */
  // getToasts(): Observable<ToastData> {
  //   return this.toastsEmitter.asObservable();
  // }
  // getClear(): Observable<number> {
  //   return this.clearEmitter.asObservable();
  // }
  /**
   * Create Toast of a default type
   */
  default(options) {
    this.add(options, "default");
  }
  /**
   * Create Toast of info type
   * @param options Individual toasta config overrides
   */
  info(options) {
    this.add(options, "info");
  }
  /**
   * Create Toast of success type
   * @param options Individual toasta config overrides
   */
  success(options) {
    this.add(options, "success");
  }
  /**
   * Create Toast of wait type
   * @param options Individual toasta config overrides
   */
  wait(options) {
    this.add(options, "wait");
  }
  /**
   * Create Toast of error type
   * @param options Individual toasta config overrides
   */
  error(options) {
    this.add(options, "error");
  }
  /**
   * Create Toast of warning type
   * @param options Individual toasta config overrides
   */
  warning(options) {
    this.add(options, "warning");
  }
  // Add a new toast item
  add(options, type) {
    let toastaOptions;
    if (isString(options) && options !== "" || isNumber(options)) {
      toastaOptions = {
        title: options.toString()
      };
    } else {
      toastaOptions = options;
    }
    if (!toastaOptions || !toastaOptions.title && !toastaOptions.msg) {
      throw new Error("ngx-toasta: No toast title or message specified!");
    }
    type = type || "default";
    this.uniqueCounter++;
    const showClose = this._checkConfigBooleanItem(this.config, toastaOptions, "showClose");
    const showDuration = this._checkConfigBooleanItem(this.config, toastaOptions, "showDuration");
    let theme;
    if (toastaOptions.theme) {
      theme = _ToastaService.THEMES.indexOf(toastaOptions.theme) > -1 ? toastaOptions.theme : this.config.theme;
    } else {
      theme = this.config.theme;
    }
    const toast = {
      id: this.uniqueCounter,
      title: toastaOptions.title,
      msg: toastaOptions.msg,
      showClose,
      showDuration,
      type: "toasta-type-" + type,
      theme: "toasta-theme-" + theme,
      // If there's a timeout individually or globally, set the toast to timeout
      // Allows a caller to pass null/0 and override the default. Can also set the default to null/0 to turn off.
      timeout: toastaOptions.hasOwnProperty("timeout") ? toastaOptions.timeout ?? 0 : this.config.timeout,
      onAdd: toastaOptions.onAdd && isFunction(toastaOptions.onAdd) ? toastaOptions.onAdd : void 0,
      onRemove: toastaOptions.onRemove && isFunction(toastaOptions.onRemove) ? toastaOptions.onRemove : void 0
    };
    this.emitEvent(new ToastaEvent(ToastaEventType.ADD, toast));
    if (toastaOptions.onAdd && isFunction(toastaOptions.onAdd)) {
      toastaOptions.onAdd.call(this, toast);
    }
  }
  // Clear all toasts
  clearAll() {
    this.emitEvent(new ToastaEvent(ToastaEventType.CLEAR_ALL));
  }
  // Clear the specific one
  clear(id) {
    this.emitEvent(new ToastaEvent(ToastaEventType.CLEAR, id));
  }
  // Checks whether the local option is set, if not,
  // checks the global config
  _checkConfigBooleanItem(config, options, property) {
    if (options[property] === false) {
      return false;
    } else if (!options[property]) {
      return config[property];
    } else {
      return true;
    }
  }
  emitEvent(event) {
    if (this.eventSource) {
      this.eventSource.next(event);
    }
  }
};
ToastaService.THEMES = ["default", "material", "bootstrap"];
ToastaService.ɵfac = function ToastaService_Factory(t) {
  return new (t || ToastaService)(ɵɵinject(ToastaConfig));
};
ToastaService.ɵprov = ɵɵdefineInjectable({
  token: ToastaService,
  factory: ToastaService.ɵfac
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastaService, [{
    type: Injectable
  }], function() {
    return [{
      type: ToastaConfig
    }];
  }, null);
})();
var SafeHtmlPipe = class {
  constructor(domSanitized) {
    this.domSanitized = domSanitized;
  }
  transform(value, ...args) {
    return this.domSanitized.bypassSecurityTrustHtml(value);
  }
};
SafeHtmlPipe.ɵfac = function SafeHtmlPipe_Factory(t) {
  return new (t || SafeHtmlPipe)(ɵɵdirectiveInject(DomSanitizer, 16));
};
SafeHtmlPipe.ɵpipe = ɵɵdefinePipe({
  name: "safeHtml",
  type: SafeHtmlPipe,
  pure: true
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(SafeHtmlPipe, [{
    type: Pipe,
    args: [{
      name: "safeHtml"
    }]
  }], function() {
    return [{
      type: DomSanitizer
    }];
  }, null);
})();
var ToastComponent = class {
  constructor() {
    this.progressPercent = 0;
    this.startTime = performance.now();
    this.closeToastEvent = new EventEmitter();
  }
  ngAfterViewInit() {
    if (this.toast.showDuration && this.toast.timeout > 0) {
      this.progressInterval = window.setInterval(() => {
        this.progressPercent = 100 - (performance.now() - this.startTime) / this.toast.timeout * 100;
        if (this.progressPercent <= 0) {
          clearInterval(this.progressInterval);
        }
      }, 16.7);
    }
  }
  /**
   * Event handler invokes when user clicks on close button.
   * This method emit new event into ToastaContainer to close it.
   */
  close($event) {
    $event.preventDefault();
    this.closeToastEvent.next(this.toast);
    if (this.progressInterval) {
      clearInterval(this.progressInterval);
    }
  }
};
ToastComponent.ɵfac = function ToastComponent_Factory(t) {
  return new (t || ToastComponent)();
};
ToastComponent.ɵcmp = ɵɵdefineComponent({
  type: ToastComponent,
  selectors: [["ngx-toast"]],
  inputs: {
    toast: "toast"
  },
  outputs: {
    closeToastEvent: "closeToast"
  },
  decls: 4,
  vars: 7,
  consts: [[1, "toast", 3, "ngClass"], ["class", "close-button", 3, "click", 4, "ngIf"], ["class", "toast-text", 4, "ngIf"], ["class", "durationbackground", 4, "ngIf"], [1, "close-button", 3, "click"], [1, "toast-text"], ["class", "toast-title", 3, "innerHTML", 4, "ngIf"], [4, "ngIf"], ["class", "toast-msg", 3, "innerHtml", 4, "ngIf"], [1, "toast-title", 3, "innerHTML"], [1, "toast-msg", 3, "innerHtml"], [1, "durationbackground"], [1, "durationbar"]],
  template: function ToastComponent_Template(rf, ctx) {
    if (rf & 1) {
      ɵɵelementStart(0, "div", 0);
      ɵɵtemplate(1, ToastComponent_div_1_Template, 1, 0, "div", 1)(2, ToastComponent_div_2_Template, 4, 3, "div", 2)(3, ToastComponent_div_3_Template, 2, 2, "div", 3);
      ɵɵelementEnd();
    }
    if (rf & 2) {
      ɵɵproperty("ngClass", ɵɵpureFunction2(4, _c0, ctx.toast.type, ctx.toast.theme));
      ɵɵadvance();
      ɵɵproperty("ngIf", ctx.toast.showClose);
      ɵɵadvance();
      ɵɵproperty("ngIf", ctx.toast.title || ctx.toast.msg);
      ɵɵadvance();
      ɵɵproperty("ngIf", ctx.toast.showDuration && ctx.toast.timeout > 0);
    }
  },
  dependencies: [NgClass, NgIf, SafeHtmlPipe],
  encapsulation: 2
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastComponent, [{
    type: Component,
    args: [{
      selector: "ngx-toast",
      template: `
        <div class="toast" [ngClass]="[toast.type, toast.theme]">
            <div *ngIf="toast.showClose" class="close-button" (click)="close($event)"></div>
            <div *ngIf="toast.title || toast.msg" class="toast-text">
                <span *ngIf="toast.title" class="toast-title" [innerHTML]="toast.title | safeHtml"></span>
                <br *ngIf="toast.title && toast.msg" />
                <span *ngIf="toast.msg" class="toast-msg" [innerHtml]="toast.msg | safeHtml"></span>
            </div>
            <div class="durationbackground" *ngIf="toast.showDuration && toast.timeout > 0">
                <div class="durationbar" [style.width.%]="progressPercent">
                </div>
            </div>
        </div>`
    }]
  }], null, {
    toast: [{
      type: Input
    }],
    closeToastEvent: [{
      type: Output,
      args: ["closeToast"]
    }]
  });
})();
var ToastaComponent = class _ToastaComponent {
  // The window position where the toast pops up. Possible values:
  // - bottom-right (default value from ToastConfig)
  // - bottom-left
  // - bottom-center
  // - bottom-fullwidth
  // - top-right
  // - top-left
  // - top-center
  // - top-fullwidth
  // - center-center
  set position(value) {
    if (value) {
      let notFound = true;
      for (let i = 0; i < _ToastaComponent.POSITIONS.length; i++) {
        if (_ToastaComponent.POSITIONS[i] === value) {
          notFound = false;
          break;
        }
      }
      if (notFound) {
        value = this.config.position;
      }
    } else {
      value = this.config.position;
    }
    this._position = "toasta-position-" + value;
  }
  get position() {
    return this._position;
  }
  constructor(config, toastaService) {
    this.config = config;
    this.toastaService = toastaService;
    this._position = "";
    this.toasts = [];
    this.position = "";
  }
  /**
   * `ngOnInit` is called right after the directive's data-bound properties have been checked for the
   * first time, and before any of its children have been checked. It is invoked only once when the
   * directive is instantiated.
   */
  ngOnInit() {
    this.toastaService.events.subscribe((event) => {
      if (event.type === ToastaEventType.ADD) {
        const toast = event.value;
        this.add(toast);
      } else if (event.type === ToastaEventType.CLEAR) {
        const id = event.value;
        this.clear(id);
      } else if (event.type === ToastaEventType.CLEAR_ALL) {
        this.clearAll();
      }
    });
  }
  /**
   * Event listener of 'closeToast' event comes from ToastaComponent.
   * This method removes ToastComponent assosiated with this Toast.
   */
  closeToast(toast) {
    this.clear(toast.id);
  }
  /**
   * Add new Toast
   */
  add(toast) {
    if (this.config.limit && this.toasts.length >= this.config.limit) {
      this.toasts.shift();
    }
    this.toasts.push(toast);
    if (+toast.timeout) {
      this._setTimeout(toast);
    }
  }
  /**
   * Clear individual toast by id
   * @param id is unique identifier of Toast
   */
  clear(id) {
    if (id) {
      this.toasts.forEach((value, key) => {
        if (value.id === id) {
          if (value.onRemove && isFunction(value.onRemove)) {
            value.onRemove.call(this, value);
          }
          this.toasts.splice(key, 1);
        }
      });
    } else {
      throw new Error("Please provide id of Toast to close");
    }
  }
  /**
   * Clear all toasts
   */
  clearAll() {
    this.toasts.forEach((value, key) => {
      if (value.onRemove && isFunction(value.onRemove)) {
        value.onRemove.call(this, value);
      }
    });
    this.toasts = [];
  }
  /**
   * Custom setTimeout function for specific setTimeouts on individual toasts.
   */
  _setTimeout(toast) {
    window.setTimeout(() => {
      this.clear(toast.id);
    }, toast.timeout);
  }
};
ToastaComponent.POSITIONS = ["bottom-right", "bottom-left", "bottom-center", "bottom-fullwidth", "top-right", "top-left", "top-center", "top-fullwidth", "center-center"];
ToastaComponent.ɵfac = function ToastaComponent_Factory(t) {
  return new (t || ToastaComponent)(ɵɵdirectiveInject(ToastaConfig), ɵɵdirectiveInject(ToastaService));
};
ToastaComponent.ɵcmp = ɵɵdefineComponent({
  type: ToastaComponent,
  selectors: [["ngx-toasta"]],
  inputs: {
    position: "position"
  },
  decls: 2,
  vars: 4,
  consts: [["id", "toasta", 3, "ngClass"], [3, "toast", "closeToast", 4, "ngFor", "ngForOf"], [3, "toast", "closeToast"]],
  template: function ToastaComponent_Template(rf, ctx) {
    if (rf & 1) {
      ɵɵelementStart(0, "div", 0);
      ɵɵtemplate(1, ToastaComponent_ngx_toast_1_Template, 1, 1, "ngx-toast", 1);
      ɵɵelementEnd();
    }
    if (rf & 2) {
      ɵɵproperty("ngClass", ɵɵpureFunction1(2, _c1, ctx.position));
      ɵɵadvance();
      ɵɵproperty("ngForOf", ctx.toasts);
    }
  },
  dependencies: [NgClass, NgForOf, ToastComponent],
  encapsulation: 2
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastaComponent, [{
    type: Component,
    args: [{
      selector: "ngx-toasta",
      template: `
    <div id="toasta" [ngClass]="[position]">
        <ngx-toast *ngFor="let toast of toasts" [toast]="toast" (closeToast)="closeToast(toast)"></ngx-toast>
    </div>`
    }]
  }], function() {
    return [{
      type: ToastaConfig
    }, {
      type: ToastaService
    }];
  }, {
    position: [{
      type: Input
    }]
  });
})();
var providers = [ToastaConfig, {
  provide: ToastaService,
  useFactory: toastaServiceFactory,
  deps: [ToastaConfig]
}];
var ToastaModule = class _ToastaModule {
  static forRoot() {
    return {
      ngModule: _ToastaModule,
      providers
    };
  }
};
ToastaModule.ɵfac = function ToastaModule_Factory(t) {
  return new (t || ToastaModule)();
};
ToastaModule.ɵmod = ɵɵdefineNgModule({
  type: ToastaModule,
  declarations: [ToastComponent, ToastaComponent, SafeHtmlPipe],
  imports: [CommonModule],
  exports: [ToastComponent, ToastaComponent]
});
ToastaModule.ɵinj = ɵɵdefineInjector({
  providers,
  imports: [CommonModule]
});
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(ToastaModule, [{
    type: NgModule,
    args: [{
      imports: [CommonModule],
      declarations: [ToastComponent, ToastaComponent, SafeHtmlPipe],
      exports: [ToastComponent, ToastaComponent],
      providers
    }]
  }], null, null);
})();
export {
  SafeHtmlPipe,
  ToastComponent,
  ToastData,
  ToastOptions,
  ToastaComponent,
  ToastaConfig,
  ToastaEvent,
  ToastaEventType,
  ToastaModule,
  ToastaService,
  providers,
  toastaServiceFactory
};
//# sourceMappingURL=ngx-toasta.js.map
