/*! 
* DevExtreme (Sparklines)
* Version: 15.2.4
* Build date: Dec 8, 2015
*
* Copyright (c) 2012 - 2015 Developer Express Inc. ALL RIGHTS RESERVED
* EULA: https://www.devexpress.com/Support/EULAs/DevExtreme.xml
*/
"use strict";if(!window.DevExpress||!DevExpress.MOD_VIZ_SPARKLINES){if(!window.DevExpress||!DevExpress.MOD_VIZ_CORE)throw Error("Required module is not referenced: viz-core");(function(n,t,i){function d(n,t){var r=n.lineSpacing,u=(r!==i&&r!==null?r:p)+n.size;return function(n){for(var f="",r=n.valueText,i=0;i<r.length;i+=2)f+="<tr><td>"+r[i]+"<\/td><td style='width: 15px'><\/td><td style='text-align: "+(t?"left":"right")+"'>"+r[i+1]+"<\/td><\/tr>";return{html:"<table style='border-spacing:0px; line-height: "+u+"px'>"+f+"<\/table>"}}}var p=2,u=200,w=1e3,h=t.require("/ui/events/ui.events.utils"),b=t.require("/ui/events/ui.events.wheel"),k=n.extend,c=Math.abs,e=t.viz,r=n.noop;t.viz.sparklines={},t.viz.sparklines.BaseSparkline=e.BaseWidget.inherit({_setDeprecatedOptions:function(){this.callBase(),n.extend(this._deprecatedOptions,{"tooltip.verticalAlignment":{since:"15.1",message:"Now tootips are aligned automatically"},"tooltip.horizontalAlignment":{since:"15.1",message:"Now tootips are aligned automatically"}})},_useLinks:!1,_clean:function(){var n=this;n._tooltipShown&&(n._tooltipShown=!1,n._tooltip.hide()),n._cleanWidgetElements(),n._cleanTranslators()},_initCore:function(){var n=this;n._tooltipTracker=n._renderer.root,n._tooltipTracker.attr({"pointer-events":"visible"}),n._createHtmlElements(),n._initTooltipEvents()},_initTooltip:function(){this._initTooltipBase=this.callBase},_getDefaultSize:function(){return this._defaultSize},_disposeCore:function(){this._disposeWidgetElements(),this._disposeTooltipEvents(),this._ranges=null},_render:function(){var n=this;n._prepareOptions(),n._updateWidgetElements(),n._drawWidgetElements()},_updateWidgetElements:function(){this._updateRange(),this._updateTranslator()},_applySize:function(){this._allOptions&&(this._allOptions.size={width:this._canvas.width,height:this._canvas.height})},_cleanTranslators:function(){this._translatorX=null,this._translatorY=null},_setupResizeHandler:r,_resize:function(){var n=this;n._redrawWidgetElements(),n._tooltipShown&&(n._tooltipShown=!1,n._tooltip.hide()),n._drawn()},_prepareOptions:function(){return k(!0,{},this._themeManager.theme(),this.option())},_createThemeManager:function(){var n=new e.BaseThemeManager;return n._themeSection=this._widgetType,n._fontFields=["tooltip.font"],n},_getTooltipCoords:function(){var n=this._canvas,t=this._renderer.getRootOffset();return{x:n.width/2+t.left,y:n.height/2+t.top}},_initTooltipEvents:function(){var n=this,t={widget:n};n._showTooltipCallback=function(){var t;n._showTooltipTimeout=null,n._tooltipShown||(n._tooltipShown=!0,t=n._getTooltip(),t.isEnabled()&&n._tooltip.show(n._getTooltipData(),n._getTooltipCoords(),{}))},n._hideTooltipCallback=function(){n._hideTooltipTimeout=null,n._tooltipShown&&(n._tooltipShown=!1,n._tooltip.hide())},n._disposeCallbacks=function(){n=n._showTooltipCallback=n._hideTooltipCallback=n._disposeCallbacks=null};n._tooltipTracker.on(nt,t).on(tt,t).on(l,t);n._tooltipTracker.on(g)},_disposeTooltipEvents:function(){var n=this;clearTimeout(n._showTooltipTimeout),clearTimeout(n._hideTooltipTimeout),n._tooltipTracker.off(),n._disposeCallbacks()},_updateTranslator:function(){var n=this,t=this._canvas,i=this._ranges;n._translatorX=new e.Translator2D(i.arg,t,{isHorizontal:!0}),n._translatorY=new e.Translator2D(i.val,t)},_getTooltip:function(){var n=this;return n._tooltip||(n._initTooltipBase(),n._setTooltipRendererOptions(n._tooltipRendererOptions),n._tooltipRendererOptions=null,n._setTooltipOptions()),n._tooltip},_setTooltipRendererOptions:function(n){this._tooltip?this._tooltip.setRendererOptions(n):this._tooltipRendererOptions=n},_setTooltipOptions:function(){var i=this._tooltip,t=i&&this._getOption("tooltip");i&&i.update(n.extend({},t,{customizeTooltip:n.isFunction(t.customizeTooltip)?t.customizeTooltip:d(t.font,this.option("rtlEnabled")),enabled:t.enabled&&this._isTooltipEnabled()}))},_showTooltip:function(n){var t=this;clearTimeout(t._hideTooltipTimeout),t._hideTooltipTimeout=null,clearTimeout(t._showTooltipTimeout),t._showTooltipTimeout=setTimeout(t._showTooltipCallback,n)},_hideTooltip:function(n){var t=this;clearTimeout(t._showTooltipTimeout),t._showTooltipTimeout=null,clearTimeout(t._hideTooltipTimeout),n?t._hideTooltipTimeout=setTimeout(t._hideTooltipCallback,n):t._hideTooltipCallback()},_endLoading:function(n){n()},_initTitle:r,_updateTitle:r,_initLoadingIndicator:r,_disposeLoadingIndicator:r,_updateLoadingIndicatorOptions:r,_updateLoadingIndicatorSize:r,_scheduleLoadingIndicatorHiding:r,showLoadingIndicator:r,hideLoadingIndicator:r});var g={"contextmenu.sparkline-tooltip":function(n){(h.isTouchEvent(n)||h.isPointerEvent(n))&&n.preventDefault()},"MSHoldVisual.sparkline-tooltip":function(n){n.preventDefault()}},nt={"mouseover.sparkline-tooltip":function(n){s=!1;var t=n.data.widget;t._x=n.pageX,t._y=n.pageY;t._tooltipTracker.off(o).on(o,n.data);t._showTooltip(u)},"mouseout.sparkline-tooltip":function(n){if(!s){var t=n.data.widget;t._tooltipTracker.off(o),t._hideTooltip(u)}}},l={};l[b.name+".sparkline-tooltip"]=function(n){n.data.widget._hideTooltip()};var o={"mousemove.sparkline-tooltip":function(n){var t=n.data.widget;t._showTooltipTimeout&&(c(t._x-n.pageX)>3||c(t._y-n.pageY)>3)&&(t._x=n.pageX,t._y=n.pageY,t._showTooltip(u))}},f=null,a=function(n){n.preventDefault();var t=f;t&&t!==n.data.widget&&t._hideTooltip(u),t=f=n.data.widget,t._showTooltip(w),t._touch=!0},v=function(){var n=f;n&&(n._touch||(n._hideTooltip(u),f=null),n._touch=null)},y=function(){var n=f;n&&n._showTooltipTimeout&&(n._hideTooltip(u),f=null)},s=!1,tt={"pointerdown.sparkline-tooltip":a,"touchstart.sparkline-tooltip":a};n(document).on({"pointerdown.sparkline-tooltip":function(){s=!0,v()},"touchstart.sparkline-tooltip":v,"pointerup.sparkline-tooltip":y,"touchend.sparkline-tooltip":y})})(jQuery,DevExpress),function(n,t,i){var r=t.viz,p=t.require("/utils/utils.common"),w=t.require("/componentRegistrator"),o=1,s=50,b=4,k=250,d=30,h=5,c=3,g={line:!0,spline:!0,stepline:!0,area:!0,steparea:!0,splinearea:!0,bar:!0,winloss:!0},u=Math,nt=u.abs,tt=u.round,l=u.max,a=u.min,f=isFinite,v=r.utils.map,it=r.utils.normalizeEnum,y=p.isDefined,e=Number,rt=String;w("dxSparkline",r.sparklines,r.sparklines.BaseSparkline.inherit({_rootClassPrefix:"dxsl",_rootClass:"dxsl-sparkline",_widgetType:"sparkline",_invalidatingOptions:["lineColor","lineWidth","areaOpacity","minColor","maxColor","barPositiveColor","barNegativeColor","winColor","lessColor","firstLastColor","pointSymbol","pointColor","pointSize","type","argumentField","valueField","winlossThreshold","showFirstLast","showMinMax","ignoreEmptyPoints","minValue","maxValue"],_defaultSize:{width:k,height:d,left:h,right:h,top:c,bottom:c},_initCore:function(){this.callBase(),this._updateDataSource(),this._createSeries()},_dataSourceChangedHandler:function(){this._initialized&&(this._clean(),this._updateWidgetElements(),this._drawWidgetElements())},_updateWidgetElements:function(){this._updateSeries(),this.callBase()},_redrawWidgetElements:function(){var n=this;n._updateTranslator(),n._correctPoints(),n._series.draw({x:n._translatorX,y:n._translatorY}),n._seriesGroup.append(n._renderer.root)},_disposeWidgetElements:function(){var n=this;n._series&&n._series.dispose(),n._series=n._seriesGroup=n._seriesLabelGroup=null},_cleanWidgetElements:function(){this._seriesGroup.remove(),this._seriesLabelGroup.remove(),this._seriesGroup.clear(),this._seriesLabelGroup.clear()},_drawWidgetElements:function(){this._dataSource.isLoaded()&&(this._drawSeries(),this._drawn())},_prepareOptions:function(){var n=this;n._allOptions=n.callBase(),n._allOptions.type=it(n._allOptions.type),g[n._allOptions.type]||(n._allOptions.type="line")},_createHtmlElements:function(){this._seriesGroup=this._renderer.g().attr({"class":"dxsl-series"}),this._seriesLabelGroup=this._renderer.g().attr({"class":"dxsl-series-labels"})},_createSeries:function(){this._series=new r.series.Series({renderer:this._renderer,seriesGroup:this._seriesGroup,labelsGroup:this._seriesLabelGroup},{widgetType:"chart",type:"line"})},_updateSeries:function(){var n=this,t,u;n._prepareDataSource(),u=n._prepareSeriesOptions(),n._series.updateOptions(u),t=[[n._series]],t.argumentOptions={type:u.type==="bar"?"discrete":i},n._simpleDataSource=r.validateData(n._simpleDataSource,t,n._incidentOccured,{checkTypeForAllData:!1,convertToAxisDataType:!0,sortingMethod:!0}),n._series.updateData(n._simpleDataSource)},_handleChangedOptions:function(n){var t=this;t.callBase.apply(t,arguments),"dataSource"in n&&t._allOptions&&t._updateDataSource()},_parseNumericDataSource:function(n,t,r){var u=this.option("ignoreEmptyPoints");return v(n,function(n,o){var s=null,c,h;return n!==i&&(s={},c=f(n),s[t]=c?rt(o):n[t],h=c?n:n[r],s[r]=h===null?u?i:h:e(h),s=s[t]!==i&&s[r]!==i?s:null),s})},_parseWinlossDataSource:function(n,t,i){var u=-1,f=0,e=1,o=.0001,r=this._allOptions.winlossThreshold;return v(n,function(n){var s={};return s[t]=n[t],s[i]=nt(n[i]-r)<o?f:n[i]>r?e:u,s})},_prepareDataSource:function(){var n=this,t=n._allOptions,r=t.argumentField,u=t.valueField,f=n._dataSource.items()||[],i=n._parseNumericDataSource(f,r,u);t.type==="winloss"?(n._winlossDataSource=i,n._simpleDataSource=n._parseWinlossDataSource(i,r,u)):n._simpleDataSource=i},_prepareSeriesOptions:function(){var t=this,n=t._allOptions,r=n.type==="winloss"?"bar":n.type;return{visible:!0,argumentField:n.argumentField,valueField:n.valueField,color:n.lineColor,width:n.lineWidth,widgetType:"chart",type:r,opacity:r.indexOf("area")!==-1?t._allOptions.areaOpacity:i,customizePoint:t._getCustomizeFunction(),point:{size:n.pointSize,symbol:n.pointSymbol,border:{visible:!0,width:2},color:n.pointColor,visible:!1,hoverStyle:{border:{}},selectionStyle:{border:{}}},border:{color:n.lineColor,width:n.lineWidth,visible:r!=="bar"}}},_createBarCustomizeFunction:function(n){var i=this,t=i._allOptions,r=i._winlossDataSource;return function(){var u=this.index,f=t.type==="winloss",e=f?t.winlossThreshold:0,o=f?r[u][t.valueField]:this.value,s=f?t.winColor:t.barPositiveColor,h=f?t.lossColor:t.barNegativeColor,i;return i=o>=e?s:h,(u===n.first||u===n.last)&&(i=t.firstLastColor),u===n.min&&(i=t.minColor),u===n.max&&(i=t.maxColor),{color:i}}},_createLineCustomizeFunction:function(n){var i=this,t=i._allOptions;return function(){var i,r=this.index;return(r===n.first||r===n.last)&&(i=t.firstLastColor),r===n.min&&(i=t.minColor),r===n.max&&(i=t.maxColor),i?{visible:!0,border:{color:i}}:{}}},_getCustomizeFunction:function(){var n=this,i=n._allOptions,u=n._winlossDataSource||n._simpleDataSource,r=n._getExtremumPointsIndexes(u),t;return t=i.type==="winloss"||i.type==="bar"?n._createBarCustomizeFunction(r):n._createLineCustomizeFunction(r)},_getExtremumPointsIndexes:function(n){var t=this,r=t._allOptions,u=n.length-1,i={};return t._minMaxIndexes=t._findMinMax(n),r.showFirstLast&&(i.first=0,i.last=u),r.showMinMax&&(i.min=t._minMaxIndexes.minIndex,i.max=t._minMaxIndexes.maxIndex),i},_findMinMax:function(n){for(var h=this,r=h._allOptions.valueField,c=n[0]||{},u=c[r]||0,f=u,e=u,o=0,s=0,l=n.length,i,t=1;t<l;t++)i=n[t][r],i<f&&(f=i,o=t),i>e&&(e=i,s=t);return{minIndex:o,maxIndex:s}},_updateRange:function(){var o=this,c=o._series,v=c.type,w=v==="bar",b=v==="winloss",k=.15,d=.1,g=1,nt=-1,n=c.getRangeData(),t=o._allOptions.minValue,s=y(t)&&f(t),r=o._allOptions.maxValue,h=y(r)&&f(r),u,p;u=(n.val.max-n.val.min)*k,w||b||v==="area"?(n.val.min!==0&&(n.val.min-=u),n.val.max!==0&&(n.val.max+=u)):(n.val.min-=u,n.val.max+=u),(s||h)&&(s&&h?(n.val.minVisible=a(t,r),n.val.maxVisible=l(t,r)):(n.val.minVisible=s?e(t):i,n.val.maxVisible=h?e(r):i),b&&(n.val.minVisible=s?l(n.val.minVisible,nt):i,n.val.maxVisible=h?a(n.val.maxVisible,g):i)),c.getPoints().length>1&&(w?(p=(n.arg.max-n.arg.min)*d,n.arg.min=n.arg.min-p,n.arg.max=n.arg.max+p):n.arg.stick=!0),o._ranges=n},_getBarWidth:function(n){var r=this,i=r._canvas,u=n*b,f=i.width-i.left-i.right-u,t=tt(f/n);return t<o&&(t=o),t>s&&(t=s),t},_correctPoints:function(){var t=this,i=t._allOptions.type,r=t._series.getPoints(),u=r.length,f,n;if(i==="bar"||i==="winloss")for(f=t._getBarWidth(u),n=0;n<u;n++)r[n].correctCoordinates({width:f,offset:0})},_drawSeries:function(){var n=this;n._simpleDataSource.length>0&&(n._correctPoints(),n._series.draw({x:n._translatorX,y:n._translatorY}),n._seriesGroup.append(n._renderer.root))},_isTooltipEnabled:function(){return!!this._simpleDataSource.length},_getTooltipData:function(){var t=this,r=t._allOptions,n=t._winlossDataSource||t._simpleDataSource,i=t._tooltip;if(n.length===0)return{};var e=t._minMaxIndexes,u=r.valueField,o=n[0][u],s=n[n.length-1][u],h=n[e.minIndex][u],c=n[e.maxIndex][u],l=i.formatValue(o),a=i.formatValue(s),v=i.formatValue(h),y=i.formatValue(c),f={firstValue:l,lastValue:a,minValue:v,maxValue:y,originalFirstValue:o,originalLastValue:s,originalMinValue:h,originalMaxValue:c,valueText:["Start:",l,"End:",a,"Min:",v,"Max:",y]};return r.type==="winloss"&&(f.originalThresholdValue=r.winlossThreshold,f.thresholdValue=i.formatValue(r.winlossThreshold)),f}}))}(jQuery,DevExpress),function(n,t,i){var h=t.require("/componentRegistrator"),f=.02,e=.98,c=.1,l=.9,a=300,v=30,o=1,s=2,r=Number,u=isFinite;h("dxBullet",t.viz.sparklines,t.viz.sparklines.BaseSparkline.inherit({_rootClassPrefix:"dxb",_rootClass:"dxb-bullet",_widgetType:"bullet",_invalidatingOptions:["color","targetColor","targetWidth","showTarget","showZeroLevel","value","target","startScaleValue","endScaleValue"],_defaultSize:{width:a,height:v,left:o,right:o,top:s,bottom:s},_disposeWidgetElements:function(){delete this._zeroLevelPath,delete this._targetPath,delete this._barValuePath},_redrawWidgetElements:function(){this._updateTranslator(),this._drawBarValue(),this._drawTarget(),this._drawZeroLevel()},_cleanWidgetElements:function(){this._zeroLevelPath.remove(),this._targetPath.remove(),this._barValuePath.remove()},_drawWidgetElements:function(){this._drawBullet(),this._drawn()},_createHtmlElements:function(){var n=this._renderer;this._zeroLevelPath=n.path(i,"line").attr({"class":"dxb-zero-level","stroke-linecap":"square"}),this._targetPath=n.path(i,"line").attr({"class":"dxb-target","stroke-linecap":"square"}),this._barValuePath=n.path(i,"line").attr({"class":"dxb-bar-value","stroke-linecap":"square"})},_prepareOptions:function(){var n=this,t,e,o,c,u,f,s,h;n._allOptions=t=n.callBase(),s=n._allOptions.value===i,h=n._allOptions.target===i,n._tooltipEnabled=!(s&&h),s&&(n._allOptions.value=0),h&&(n._allOptions.target=0),t.value=u=r(t.value),t.target=f=r(t.target),n._allOptions.startScaleValue===i&&(n._allOptions.startScaleValue=f<u?f:u,n._allOptions.startScaleValue=n._allOptions.startScaleValue<0?n._allOptions.startScaleValue:0),n._allOptions.endScaleValue===i&&(n._allOptions.endScaleValue=f>u?f:u),t.startScaleValue=e=r(t.startScaleValue),t.endScaleValue=o=r(t.endScaleValue),o<e&&(c=o,n._allOptions.endScaleValue=e,n._allOptions.startScaleValue=c,n._allOptions.inverted=!0)},_updateRange:function(){var t=this,n=t._allOptions;t._ranges={arg:{invert:n.inverted,min:n.startScaleValue,max:n.endScaleValue,axisType:"continuous",dataType:"numeric"},val:{min:0,max:1,axisType:"continuous",dataType:"numeric"}}},_drawBullet:function(){var t=this,n=t._allOptions,i=n.startScaleValue!==n.endScaleValue,r=u(n.startScaleValue),f=u(n.endScaleValue),e=u(n.value),o=u(n.target);i&&f&&r&&o&&e&&(this._drawBarValue(),this._drawTarget(),this._drawZeroLevel())},_getTargetParams:function(){var n=this,t=n._allOptions,i=n._translatorY,r=n._translatorX.translate(t.target);return{points:[r,i.translate(f),r,i.translate(e)],stroke:t.targetColor,"stroke-width":t.targetWidth}},_getBarValueParams:function(){var e=this,r=e._allOptions,o=e._translatorX,s=e._translatorY,u=r.startScaleValue,f=r.endScaleValue,t=r.value,h=s.translate(c),a=s.translate(l),n,i;return t>0?(n=u<=0?0:u,i=t>=f?f:t<n?n:t):(n=f>=0?0:f,i=t<u?u:t>n?n:t),n=o.translate(n),i=o.translate(i),{points:[n,a,i,a,i,h,n,h],fill:r.color}},_getZeroLevelParams:function(){var n=this,t=n._translatorY,i=n._translatorX.translate(0);return{points:[i,t.translate(f),i,t.translate(e)],stroke:n._allOptions.targetColor,"stroke-width":1}},_drawZeroLevel:function(){var n=this,t=n._allOptions;0>t.endScaleValue||0<t.startScaleValue||!t.showZeroLevel||n._zeroLevelPath.attr(n._getZeroLevelParams()).sharp().append(n._renderer.root)},_drawTarget:function(){var n=this,t=n._allOptions,i=t.target;i>t.endScaleValue||i<t.startScaleValue||!t.showTarget||n._targetPath.attr(n._getTargetParams()).sharp().append(n._renderer.root)},_drawBarValue:function(){this._barValuePath.attr(this._getBarValueParams()).append(this._renderer.root)},_getTooltipCoords:function(){var i=this._canvas,n=this._renderer.getRootOffset(),t=this._barValuePath.getBBox();return{x:t.x+t.width/2+n.left,y:i.height/2+n.top}},_getTooltipData:function(){var i=this,r=i._tooltip,u=i._allOptions,n=u.value,t=u.target;return{originalValue:n,originalTarget:t,value:r.formatValue(n),target:r.formatValue(t),valueText:["Actual Value:",n,"Target Value:",t]}},_isTooltipEnabled:function(){return this._tooltipEnabled},_initDataSource:n.noop,_disposeDataSource:n.noop}))}(jQuery,DevExpress),DevExpress.MOD_VIZ_SPARKLINES=!0}