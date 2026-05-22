{==============================================================================

  Cool Flat Theme
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: tethemecoolflat.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit tethemecoolflat;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus,
  Grids, te_controls, KsThemeThemes, tethemeflat;

type

{ TTeThemeCoolFlat class }

  TTeThemeCoolFlat = class(TTeThemeFlat)
  private
  protected
  public
    function GetMetrix(Metrix: TTeThemeMetrix): integer; override;
    function GetColor(Color: TTeThemeColor): TColor; override;

    procedure DrawSysButton(Canvas: TCanvas; R: TRect;
      Kind: TTeThemeSysButton; Hot, Down, Active: boolean; BorderStyle: TTeBorderStyle); override;
    procedure DrawWindow(Canvas: TCanvas; Width, Height: integer;
      ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle); override;

    { Return theme's name (need for selecting in IDE) }
    class function GetThemeName: string; override;
  published
  end;

implementation {===============================================================}

uses ksthemeengine;

{ Metrix ======================================================================}

function TTeThemeCoolFlat.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaption: Result := $009CDEF7;
    ngcInactiveCaption: Result := clBtnFace;
    ngcCaptionShadow: Result := clBlack;
    ngcCaptionText: Result := RGB(0, 0, 0);
    ngcInactiveCaptionText: Result := RGB(0, 0, 0);

    ngcBorder: Result := $008396A0;
    ngcWindow: Result := clWhite;
    ngcBtnFace: Result := clBtnFace;
    ngcHighlight: Result := $00996633;
    ngcHotHighlight: Result := $00E1EAEB;
    { Menus }
    ngcMenuBorder: Result := $008396A0;
    ngcMenuBar: Result := clBtnFace;
    ngcMenuBarHighlight: Result := $009CDEF7;
    ngcMenuItem: Result := clBtnFace;
    ngcMenuItemHighlight: Result := $009CDEF7;
    ngcMenuBarText: Result := RGB(0, 0, 0);
    ngcMenuBarHighlightText: Result := RGB(0, 0, 0);
    ngcMenuItemText: Result := RGB(0, 0, 0);
    ngcMenuItemHighlightText: Result := RGB(0, 0, 0);
    ngcMenuItemDisabledText: Result := RGB(128, 128, 128);
    { Controls }
    ngcWindowText: Result := RGB(0, 0, 0);
    ngcPanelCaption: Result := RGB(149, 206, 255);
    ngcPanelCaption2: Result := RGB(49, 106, 197);
    ngcDisabled: Result := RGB(254, 254, 251);
    ngcDisabledBorder: Result := RGB(197, 197, 177);
  else
    Result := clRed;
  end;

  Result := KColorToColor(ChangeHue(KColor(Result), DeltaHue));
  Result := KColorToColor(ChangeBrightness(KColor(Result), DeltaBrightness));
end;

function TTeThemeCoolFlat.GetMetrix(Metrix: TTeThemeMetrix): integer;
begin
  case Metrix of
    ngmBorderWidth: Result := 3;
    ngmSmBorderWidth: Result := 3;
    ngmCaptionHeight: Result := 23;
    ngmSmCaptionHeight: Result := 18;
    ngmCaptionMargin: Result := 10;
    ngmButtonWidth: Result := 20;
    ngmButtonHeight: Result := 20;
    ngmSmButtonWidth: Result := 15;
    ngmSmButtonHeight: Result := 15;
    ngmButtonMarginX: Result := 3;
    ngmButtonMarginY: Result := 3;
    ngmButtonSpace: Result := 2;
    ngmSmButtonMarginX: Result := 3;
    ngmSmButtonMarginY: Result := 3;
    { Menus }
    ngmMenuBarHeight: Result := 22;
    ngmMenuItemHeight: Result := 17;
    { Controls }
    ngmCheckBoxWidth: Result := 13;
    ngmCheckBoxHeight: Result := 13;
    ngmRadioButtonWidth: Result := 13;
    ngmRadioButtonHeight: Result := 13;
    ngmTrackThumbWidth: Result := 9;
    ngmTrackThumbHeight: Result := 18;
    ngmTrackBarHeight: Result := 4;
    ngmPanelCaptionHeight: Result := 18;
    ngmScrollBarHeight: Result := 17;
    ngmSliderWidth: Result := 17;
    ngmTabMargin: Result := 1;
    ngmTabHeight: Result := 18;
    ngmComboButtonWidth: Result := 15;
    ngmGrabberSize: Result := 11;
  else
    Result := 0;
  end;
end;

{ Form Draw ===================================================================}

procedure TTeThemeCoolFlat.DrawSysButton(Canvas: TCanvas; R: TRect; Kind: TTeThemeSysButton;
  Hot, Down, Active: boolean; BorderStyle: TTeBorderStyle);
var
  Flag: integer;
  BR: TRect;
  Color: TColor;
begin
  { Draw face }
  BR := R;
  if Down then
    Color := GetColor(ngcHighlight)
  else
    if Hot then
      Color := GetColor(ngcHotHighlight)
    else
      Color := GetColor(ngcBtnFace);

  FillRect(Canvas, BR, Color);
  DrawRect(Canvas, BR, GetColor(ngcBorder));
  InflateRect(BR, -1, -1);
  if Down then
    DrawEdge(Canvas, BR, SunkenColor(Color, 30), RaisedColor(Color, 30))
  else
    if Hot then
      DrawEdge(Canvas, BR, RaisedColor(Color, 30), SunkenColor(Color, 30))
    else
      DrawRect(Canvas, BR, Color);
  { Draw glyph }
  BR := R;
  Flag := 0;
  if Down then Flag := Flag or DFCS_PUSHED;

  InflateRect(R, -2, -3);

  case Kind of
    ngbClose: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONCLOSE or Flag, clBlack);
    ngbHelp: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONHELP or Flag, clBlack);
    ngbMax: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONMAX or Flag, clBlack);
    ngbMin: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONMIN or Flag, clBlack);
    ngbRestore: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONRESTORE or Flag, clBlack);
    ngbRollUp, ngbRollDown:
      begin
        InflateRect(R, -4, -5);
        if not Down then
          OffsetRect(R, -1, -1);

        Canvas.Brush.Color := clBlack;
        Canvas.Pen.Color := clBlack;
        if Kind = ngbRollDown then
        begin
          Canvas.Polygon([Point(R.Left, R.Top), Point(R.Right, R.Top),
             Point((R.Left + R.Right) div 2, R.Bottom)])
        end
        else
        begin
          Canvas.Polygon([Point(R.Left, R.Bottom), Point(R.Right, R.Bottom),
            Point((R.Left + R.Right) div 2, R.Top)])
        end;
      end;
    ngbTray:
      begin
        InflateRect(R, -5, -5);
        OffsetRect(R, 0, 2);
        if Down then
          OffsetRect(R, 1, 1);

        Canvas.Brush.Color := clBlack;
        Canvas.Pen.Color := clBlack;
        Canvas.RoundRect(R.Left, R.Top, R.Right, R.Bottom, 5, 5);
      end;
  end;
end;

procedure TTeThemeCoolFlat.DrawWindow(Canvas: TCanvas; Width,
  Height: integer; ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle);
var
  BorderRect: TRect;
  CaptionRect: TRect;
  Color: TColor;
begin
  CaptionRect := ClientRect;
  CaptionRect.Bottom := CaptionRect.Top;
  CaptionRect.Top := ClientRect.Left;
  { Draw Caption }
  if Active then
    Color := GetColor(ngcCaption)
  else
    Color := GetColor(ngcInactiveCaption);

  FillRect(Canvas, CaptionRect, Color);
  DrawRect(Canvas, CaptionRect, GetColor(ngcBorder));

  { Draw Border }
  BorderRect := Rect(0, 0, Width, Height);
  DrawRect(Canvas, BorderRect, GetColor(ngcBorder));
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, GetColor(ngcBtnFace));
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, GetColor(ngcBtnFace));
end;

class function TTeThemeCoolFlat.GetThemeName: string;
begin
  Result := 'Cool Flat';
end;

initialization
  RegisterTheme(TTeThemeCoolFlat);
end.




