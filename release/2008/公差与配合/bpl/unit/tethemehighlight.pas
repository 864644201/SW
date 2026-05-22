{==============================================================================

  Base Themes
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: tethemehighlight.pas,v 1.4 2002/10/28 21:04:02 Evgeny Exp $

===============================================================================}

unit tethemehighlight;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus,
  Grids, te_controls, KsThemeThemes;

type

{ TTeThemeHighlight class }

  TTeThemeHighlight = class(TTeTheme)
  private
  protected
    procedure ChangeThemeColors; override;
  public
    constructor Create; override;
    destructor Destroy; override;

    function GetMetrix(Metrix: TTeThemeMetrix): integer; override;
    function GetColor(Color: TTeThemeColor): TColor; override;
    function GetFontName(Font: TTeThemeFont): string; override;
    function GetFontSize(Font: TTeThemeFont): integer; override;
    function GetFontStyle(Font: TTeThemeFont): TFontStyles; override;

    procedure DrawSysButton(Canvas: TCanvas; R: TRect;
      Kind: TTeThemeSysButton; Hot, Down, Active: boolean; BorderStyle: TTeBorderStyle); override;
    procedure DrawWindow(Canvas: TCanvas; Width, Height: integer;
      ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle); override;
    procedure CalcMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem; var AWidth, AHeight: integer); override;
    procedure CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem; var AWidth, AHeight: integer); override;
    procedure DrawMenuBar(Canvas: TCanvas; Width, Height: integer); override;
    procedure DrawMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); override;
    procedure DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); override;
    procedure DrawPopupMenu(Canvas: TCanvas; Width, Height: integer); override;
    procedure DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); override;
    procedure DrawMenuScrollButton(Canvas: TCanvas; Rect: TRect;
      Button: TTeMenuScrollButton; Active: boolean); override;

    procedure DrawButton(Canvas: TCanvas; Width, Height: integer;
      State: TTeThemeButtonState); override;

    procedure DrawCheckBox(Canvas: TCanvas; Rect: TRect;
      ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState); override;
    procedure DrawRadioButton(Canvas: TCanvas; Rect: TRect;
      ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState); override;

    procedure DrawTrackBarThumb(Canvas: TCanvas; R: TRect; Orientation: TTrackOrientation;
      TickMarks: TTickMark; State: TTeThemeButtonState); override;
    procedure DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect); override;

    procedure DrawProgessFrame(Canvas: TCanvas; R: TRect); override;
    procedure DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
      Orientation: TTeBarOrientation; Smooth: boolean); override;

    procedure DrawPanel(Canvas: TCanvas; R: TRect; ShowBevel, ShowCaption: boolean); override;
    procedure DrawPanelCaption(Canvas: TCanvas; R: TRect; Caption: WideString); override;
    procedure DrawPanelButton(Canvas: TCanvas;  R: TRect; Kind: TTePanelButtonKind;
      State: TTeThemeButtonState; Rolled: boolean); override;

    procedure DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect; Caption: WideString); override;

    procedure DrawScrollBar(Canvas: TCanvas; R: TRect; Kind: TScrollBarKind); override;
    procedure DrawScrollBarButton(Canvas: TCanvas; R: TRect; Kind: TScrollBarKind;
      LeftTop: boolean; State: TTeThemeButtonState); override;
    procedure DrawScrollBarSlider(Canvas: TCanvas; R: TRect; Kind: TScrollBarKind;
      State: TTeThemeButtonState); override;

    procedure DrawTabBorder(Canvas: TCanvas; R: TRect); override;
    procedure DrawTab(Canvas: TCanvas; R: TRect; TabPosition: TTabPosition;
      State: TTeThemeButtonState); override;
    procedure DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
      TabPosition: TTabPosition; State: TTeThemeButtonState); override;

    procedure DrawControlFrame(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState); override;

    procedure DrawComboButton(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState); override;

    procedure DrawSpeedButton(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState;
      Flat, Exclusive: boolean); override;
    procedure DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState;
      Flat, Exclusive: boolean); override;

    procedure DrawSpinButton(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState; Up: boolean); override;

    procedure DrawControlBar(Canvas: TCanvas; R: TRect); override;
    procedure DrawControlBarFrame(Canvas: TCanvas; R, GrabberRect: TRect); override;

    procedure DrawToolbar(Canvas: TCanvas; R: TRect); override;

    procedure DrawGridCell(Canvas: TCanvas; R: TRect; State: TGridDrawState); override;

    procedure DrawSplitter(Canvas: TCanvas; ARect: TRect; AHot, ABeveled: boolean); override;

    procedure DrawHeaderSection(Canvas: TCanvas; ARect: TRect; Section: TTeHeaderSection;
      AState: TTeSectionState); override;

    procedure DrawStatusBar(Canvas: TCanvas; ARect: TRect); override;
    procedure DrawStatusGripper(Canvas: TCanvas; ARect: TRect); override;
    procedure DrawStatusPanel(Canvas: TCanvas; ARect: TRect; Panel: TTeStatusPanel); override;

    procedure DrawScrollBox(Canvas: TCanvas; R: TRect; Enabled: boolean); override;
    
    function GetRegion(Width, Height: integer; BorderStyle: TTeBorderStyle): HRgn; override;
    { Return theme's name (need for selecting in IDE) }
    class function GetThemeName: string; override;
    class function UseTheme: boolean; override;
  published
  end;

implementation {===============================================================}

uses KsThemeEngine;

procedure DrawLine(Canvas: TCanvas; R: TRect; Color: TColor);
begin
  MoveTo(Canvas, R.Left, R.Top);
  LineTo(Canvas, R.Right, R.Bottom, Color);
end;

{ TTeThemeHighlight }

constructor TTeThemeHighlight.Create;
begin
  inherited Create;
end;

destructor TTeThemeHighlight.Destroy;
begin
  inherited Destroy;
end;

{ Metrix ======================================================================}

function TTeThemeHighlight.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaption: Result := clActiveCaption;
    ngcInactiveCaption: Result := clInactiveCaption;
    ngcCaptionShadow: Result := clBlack;
    ngcCaptionText: Result := clWhite;
    ngcInactiveCaptionText: Result := clWhite;
    ngcBorder: Result := KColorToColor(ckBorder);
    ngcWindow: Result := KColorToColor(ckWindow);
    ngcBtnFace: Result := clBtnFace;
    ngcHighlight: Result := KColorToColor(ckHighlight);
    ngcHotHighlight: Result := KColorToColor(ckHotHighlight);
    { Menus }
    ngcMenuBorder: Result := KColorToColor(ckBorder);
    ngcMenuBar: Result := clBtnFace;
    ngcMenuBarHighlight: Result := KColorToColor(ckHighlight);
    ngcMenuItem: Result := clBtnFace;
    ngcMenuItemHighlight: Result := KColorToColor(ckHighlight);
    ngcMenuBarText: Result := RGB(0, 0, 0);
    ngcMenuBarHighlightText: Result := RGB(255, 255, 255);
    ngcMenuItemText: Result := RGB(0, 0, 0);
    ngcMenuItemHighlightText: Result := RGB(255, 255, 255);
    ngcMenuItemDisabledText: Result := RGB(128, 128, 128);
    { Controls }
    ngcWindowText: Result := RGB(0, 0, 0);
    ngcPanelCaption: Result := RGB(149, 206, 255);
    ngcPanelCaption2: Result := RGB(49, 106, 197);
    ngcDisabled: Result := RGB(254, 254, 251);
    ngcDisabledBorder: Result := RGB(197, 197, 177);
  else
    Result := ckRed;
  end;

  Result := KColorToColor(ChangeHue(KColor(Result), DeltaHue));
  Result := KColorToColor(ChangeBrightness(KColor(Result), DeltaBrightness));
end;

function TTeThemeHighlight.GetFontName(Font: TTeThemeFont): string;
begin
  case Font of
    ngfCaptionText: Result := 'Tahoma';
    ngfSmCaptionText: Result := 'Tahoma';
    { Menus }
    ngfMenuBarText: Result := 'Tahoma';
    ngfMenuItemText: Result := 'Tahoma';
    { Controls }
    ngfWindowText: Result := 'Tahoma';
  else
    Result := 'Tahoma';
  end;
end;

function TTeThemeHighlight.GetFontSize(Font: TTeThemeFont): integer;
begin
  case Font of
    ngfCaptionText: Result := 9;
    ngfSmCaptionText: Result := 8;
    { Menus }
    ngfMenuBarText: Result := 8;
    ngfMenuItemText: Result := 8;
    { Contorls }
    ngfWindowText: Result := 8;
  else
    Result := 8;
  end;
end;

function TTeThemeHighlight.GetFontStyle(Font: TTeThemeFont): TFontStyles;
begin
  case Font of
    ngfCaptionText: Result := [fsBold];
    ngfSmCaptionText: Result := [fsBold];
    { Menus }
    ngfMenuBarText: Result := [];
    ngfMenuItemText: Result := [];
    { Controls }
    ngfWindowText: Result := [];
  else
    Result := [];
  end;
end;

function TTeThemeHighlight.GetMetrix(Metrix: TTeThemeMetrix): integer;
begin
  case Metrix of
    ngmBorderWidth: Result := 4;
    ngmSmBorderWidth: Result := 4;
    ngmCaptionHeight: Result := 26;  
    ngmSmCaptionHeight: Result := 20;
    ngmCaptionMargin: Result := 10;
    ngmButtonWidth: Result := 20;
    ngmButtonHeight: Result := 16;
    ngmSmButtonWidth: Result := 14;
    ngmSmButtonHeight: Result := 12;
    ngmButtonMarginX: Result := 7;
    ngmButtonMarginY: Result := 7;
    ngmButtonSpace: Result := 1;
    ngmSmButtonMarginX: Result := 6;
    ngmSmButtonMarginY: Result := 6;
    { Menus }
    ngmMenuBarHeight: Result := 23;
    ngmMenuItemHeight: Result := 17;
    { Controls }
    ngmCheckBoxWidth: Result := 13;
    ngmCheckBoxHeight: Result := 13;
    ngmRadioButtonWidth: Result := 13;
    ngmRadioButtonHeight: Result := 13;
    ngmTrackThumbWidth: Result := 11;
    ngmTrackThumbHeight: Result := 22;
    ngmTrackBarHeight: Result := 4;
    ngmPanelCaptionHeight: Result := 22;
    ngmScrollBarHeight: Result := 17;
    ngmSliderWidth: Result := 17;
    ngmTabMargin: Result := 1;
    ngmTabHeight: Result := 21;
    ngmComboButtonWidth: Result := 15;
    ngmGrabberSize: Result := 11;
  else
    Result := 0;
  end;
end;

class function TTeThemeHighlight.GetThemeName: string;
begin
  Result := 'Highlight';
end;

class function TTeThemeHighlight.UseTheme: boolean;
begin
  Result := true;
end;

procedure TTeThemeHighlight.ChangeThemeColors;
begin
end;

{ Form Draw ===================================================================}

procedure TTeThemeHighlight.DrawSysButton(Canvas: TCanvas; R: TRect; Kind: TTeThemeSysButton;
  Hot, Down, Active: boolean; BorderStyle: TTeBorderStyle);
var
  Flag: integer;
  BR: TRect;
  Color: TKColor;
  Pixel: PKColor;
  i, j: integer;
begin
  BR := R;

  Flag := 0;
  if Down then
    Flag := Flag or DFCS_PUSHED;

  { change color }
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
    DrawEdge(Canvas, BR, RaisedColor(Color, 30), SunkenColor(Color, 30));

  case Kind of
    ngbClose: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONCLOSE or Flag, clBlack);
    ngbHelp: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONHELP or Flag, clBlack);
    ngbMax: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONMAX or Flag, clBlack);
    ngbMin: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONMIN or Flag, clBlack);
    ngbRestore: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONRESTORE or Flag, clBlack);
    ngbRollUp, ngbRollDown: begin
      { Draw Glyph }
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
    ngbTray: begin
      { Draw Glyph }
      InflateRect(R, -5, -5);
      OffsetRect(R, 0, 2);
      if Down then
        OffsetRect(R, 1, 1);

      Canvas.Brush.Color := clBlack;
      Canvas.Pen.Color := clBlack;
      Canvas.Ellipse(R.Left, R.Top, R.Right, R.Bottom);
    end;
  end;
end;

procedure TTeThemeHighlight.DrawWindow(Canvas: TCanvas; Width,
  Height: integer; ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle);
var
  BorderRect: TRect;
  CaptionRect: TRect;
  Color: TKColor;
begin
  CaptionRect := ClientRect;
  CaptionRect.Bottom := CaptionRect.Top;
  CaptionRect.Top := ClientRect.Left;
  { Draw Caption }
  if Active then
    Color := GetColor(ngcCaption)
  else
    Color := GetColor(ngcInactiveCaption);

  FillGradientRect(Canvas, CaptionRect, Color, RGB(200, 200, 200), false);
  DrawRoundRect(Canvas, CaptionRect, 3, GetColor(ngcBorder));

  { Draw Border }
  BorderRect := Rect(0, 0, Width, Height);

  DrawRoundRect(Canvas, BorderRect, 3, GetColor(ngcBorder));
  InflateRect(BorderRect, -1, -1);
  DrawEdge(Canvas, BorderRect, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));
  InflateRect(BorderRect, -1, -1);
  DrawEdge(Canvas, BorderRect, RaisedColor(GetColor(ngcBtnFace), 15), SunkenColor(GetColor(ngcBtnFace), 15));
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, GetColor(ngcBtnFace)); 
end;

{ Menus Draw ==================================================================}

procedure TTeThemeHighlight.CalcMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; var AWidth, AHeight: integer);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

  AWidth := TextWidth(Canvas, Item.Caption) + 12;
  AHeight := GetMetrix(ngmMenuBarHeight);
end;

procedure TTeThemeHighlight.CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
  var AWidth, AHeight: integer);
begin
  Canvas.Font.Name := GetFontName(ngfMenuItemText);
  Canvas.Font.Size := GetFontSize(ngfMenuItemText);
  Canvas.Font.Style := GetFontStyle(ngfMenuItemText);

  if Item.Caption = '-' then
    AHeight := 9
  else
    AHeight := GetMetrix(ngmMenuBarHeight);
  AWidth := GlyphWidth + TextWidth(Canvas, Item.Caption) + ItemStep + TextWidth(Canvas, ShortCutToText(Item.ShortCut)) + SubMenuWidth;
end;

procedure TTeThemeHighlight.DrawMenuBar(Canvas: TCanvas; Width,
  Height: integer);
begin
  FillRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcMenuBar));
  DrawRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcMenuBorder));
end;

procedure TTeThemeHighlight.DrawPopupMenu(Canvas: TCanvas; Width,
  Height: integer);
var
  R: TRect;
begin
  R := Rect(0, 0, Width, Height);
  DrawRect(Canvas, R, GetColor(ngcMenuItem));
  DrawRoundRect(Canvas, R, 3, GetColor(ngcMenuBorder));
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, GetColor(ngcMenuItem));
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, GetColor(ngcMenuItem));
end;

procedure TTeThemeHighlight.DrawMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; Rect: TRect; Active, Hover: boolean);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

  InflateRect(Rect, -1, -1);
  if Item.IsToolbar then
    FillRect(Canvas, Rect, GetColor(ngcBtnFace))
  else
    FillRect(Canvas, Rect, GetColor(ngcMenuBar));
  InflateRect(Rect, -1, -1);
  { Highlight }
  if Active then
  begin
    if Item.IsToolbar then
    begin
      if Hover then
        DrawSpeedButton(Canvas, Rect, ngsHot, true, false)
      else
        DrawSpeedButton(Canvas, Rect, ngsPressed, true, false);
      Canvas.Font.Color := GetColor(ngcMenuBarText);
    end
    else
    begin
      if Hover then
        FillRect(Canvas, Rect, GetColor(ngcHotHighlight))
      else
        FillRect(Canvas, Rect, GetColor(ngcMenuBarHighlight));
      Canvas.Font.Color := GetColor(ngcMenuBarHighlightText);
      { Draw Border }
      DrawRoundRect(Canvas, Rect, 3, GetColor(ngcMenuBorder));
    end;
  end
  else
    Canvas.Font.Color := GetColor(ngcMenuBarText);

  { Text }
  DrawText(Canvas, Item.Caption, Rect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;

procedure TTeThemeHighlight.DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
var
  R: TRect;
  Flags: integer;
  Color: TKColor;
  i: integer;
begin
  { MDI Items }
  InflateRect(Rect, -1, -1);
  
  FillRect(Canvas, Rect, ckMenu);

  Flags := DFCS_BUTTONPUSH;

  if Active then
    if Hover then
      Flags := Flags or DFCS_FLAT
    else
      Flags := Flags or DFCS_PUSHED;

  case Item.MDIItemKind of
     mikSysMenu: begin
       InflateRect(Rect, -1, -1);
       if Active then
       begin
         Color := GetColor(ngcMenuItemHighlight);
         FillRect(Canvas, Rect, Color);
         { Draw lines }
         for i := 0 to RectHeight(Rect) div 2 do
         begin
           R := Rect;
           R.Top := R.Top + i * 3;
           R.Bottom := R.Top + 1;
           FillRect(Canvas, R, ckBtnShadow);
         end;
         { Draw border }
         DrawRoundRect(Canvas, Rect, 3, GetColor(ngcMenuBorder));
       end
       else
       begin
         Color := GetColor(ngcMenuItem);
         FillRect(Canvas, Rect, Color);
       end;
     end;
     mikMinimize: begin
       InflateRect(Rect, -2, -2);
       Inc(Rect.Right);
       Dec(Rect.Bottom);

       Flags := Flags or DFCS_CAPTIONMIN;
       DrawFrameControl(Canvas.Handle, Rect, DFC_CAPTION, Flags);
       DrawRect(Canvas, Rect, ckBorder);
     end;
     mikRestore: begin
       InflateRect(Rect, -2, -2);
       Inc(Rect.Right);
       Dec(Rect.Bottom);

       Flags := Flags or DFCS_CAPTIONRESTORE;
       DrawFrameControl(Canvas.Handle, Rect, DFC_CAPTION, Flags);
       DrawRect(Canvas, Rect, ckBorder);
     end;
     mikClose: begin
       InflateRect(Rect, -2, -2);
       Inc(Rect.Right);
       Dec(Rect.Bottom);

       Flags := Flags or DFCS_CAPTIONCLOSE;
       DrawFrameControl(Canvas.Handle, Rect, DFC_CAPTION, Flags);
       DrawRect(Canvas, Rect, ckBorder);
     end;
  end;
end;

procedure TTeThemeHighlight.DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
const
  CheckMarkPoints: array[0..11] of TPoint = (
    { Black }
    (X: -2; Y: -2), (X: 0; Y:  0), (X:  4; Y: -4),
    (X:  4; Y: -3), (X: 0; Y:  1), (X: -2; Y: -1),
    (X: -2; Y: -2),
    { White }
    (X: -3; Y: -2), (X: -3; Y: -1), (X: 0; Y: 2),
    (X:  5; Y: -3), (X:  5; Y: -5));
var
  Color: TKColor;
  R: TRect;
  Points: array[0..11] of TPoint;
  i, X, Y: integer;
  S: string;
begin
  if Item.Caption = '-' then
  begin
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));
    InflateRect(Rect, -2, -4);
    Rect.Bottom := Rect.Top + 1;
    FillRect(Canvas, Rect, clBtnShadow);

    Exit;
  end;

  Canvas.Font.Name := GetFontName(ngfMenuItemText);
  Canvas.Font.Size := GetFontSize(ngfMenuItemText);
  Canvas.Font.Style := GetFontStyle(ngfMenuItemText);

  { Draw highlight }
  if Active then
  begin
    Color := GetColor(ngcMenuItemHighlight);
    FillRect(Canvas, Rect, Color);
    Canvas.Font.Color := GetColor(ngcMenuItemHighlightText);
    { Draw Border }
    DrawRoundRect(Canvas, Rect, 3, GetColor(ngcMenuBorder));
  end
  else
  begin
    Color := GetColor(ngcMenuItem);
    FillRect(Canvas, Rect, Color);
    Canvas.Font.Color := GetColor(ngcMenuItemText);
  end;

  { Draw Image and Checked }
  if (Item.Checked) or ((Item.ImgList <> nil) and (Item.ImageIndex >= 0)) then
  begin
    { Draw Glyph }
    R := Rect;
    R.Right := R.Left + Item.GetGlyphSize;

    if (Item.ImgList <> nil) and (Item.ImageIndex >= 0) then
    begin
      { Draw Image }
      Item.ImgList.Draw(Canvas,
        R.Left + ((R.Right - R.Left) - Item.ImgList.Width) div 2,
        R.Top + ((R.Bottom - R.Top) - Item.ImgList.Height) div 2,
        Item.ImageIndex, Item.Enabled);
    end
    else
    begin
      { Draw CheckBox }
      X := (R.Left + R.Right) div 2 - 2;
      Y := (R.Top + R.Bottom) div 2 + 1;
      System.Move(CheckMarkPoints, Points, 12 * SizeOf(TPoint));
      for i := Low(Points) to High(Points) do
      begin
        Inc (Points[I].X, X);
        Inc (Points[I].Y, Y);
      end;
      Canvas.Pen.Color := clBlack;
      Polyline(Canvas.Handle, Points[0], 7);
      Canvas.Pen.Color := clWhite;
      Polyline(Canvas.Handle, Points[7], 5);
    end;
  end;

  { Draw Caption }
  R := Rect;
  Inc(R.Left, Item.GetGlyphSize + 4);

  { Check Enabled }
  if not Item.Enabled then
    Canvas.Font.Color := GetColor(ngcMenuItemDisabledText);
  { Draw Caption }
  DrawText(Canvas, Item.Caption, R, DrawTextBiDiModeFlags(DT_VCenter or DT_SINGLELINE));
  { Draw ShortCut }
  if Item.ShortCut <> 0 then
  begin
    S := ShortCutToText(Item.ShortCut);
    Dec(R.Right, RectHeight(R));
    { Check Enabled }
    if not Item.Enabled then
      Canvas.Font.Color := GetColor(ngcMenuItemDisabledText);
    { Draw Shortcut }
    DrawText(Canvas, S, R, DrawTextBiDiModeFlags(DT_RIGHT or DT_VCenter or DT_SINGLELINE));
  end;

  { Draw Submenu Arrow }
  if Item.Count > 0 then
  begin
    R := Rect;
    Dec(R.Right, 2);
    R.Left := R.Right - SubMenuWidth;
    Rect := Classes.Rect(0, 0, 5, 5);
    RectCenter(Rect, R);

    FillRoundRect(Canvas,  Rect, 5, Canvas.Font.Color);
    DrawRoundRect(Canvas, Rect, 5, GetColor(ngcMenuBorder));
  end;
end;

procedure TTeThemeHighlight.DrawMenuScrollButton(Canvas: TCanvas;
  Rect: TRect; Button: TTeMenuScrollButton; Active: boolean);
var
  R: TRect;
begin
  Canvas.Font.Color := GetColor(ngcMenuItemText);

  if Active then
  begin
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));
    DrawRoundRect(Canvas, Rect, 3, GetColor(ngcMenuBorder));
    InflateRect(Rect, -1, -1);
    DrawEdge(Canvas, Rect, SunkenColor(GetColor(ngcMenuItem), 30), RaisedColor(GetColor(ngcMenuItem), 30));
  end
  else
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));

  { Draw arrows }
  if Button = ksbUp then
  begin
    R := Classes.Rect(0, 0, 7, 4);
    RectCenter(R, Rect);

    Canvas.Pen.Style := psSolid;
    Canvas.Pen.Color := Canvas.Font.Color;
    Canvas.Brush.Style := bsSolid;
    Canvas.Brush.Color := Canvas.Font.Color;
    with R do
      Canvas.Polygon([Point(Left, Bottom - 1),
        Point(Right - 1, Bottom - 1),
        Point((Right - 1 + Left) div 2, Top)]);
  end;

  if Button = ksbDown then
  begin
    R := Classes.Rect(0, 0, 7, 4);
    RectCenter(R, Rect);

    Canvas.Pen.Style := psSolid;
    Canvas.Pen.Color := Canvas.Font.Color;
    Canvas.Brush.Style := bsSolid;
    Canvas.Brush.Color := Canvas.Font.Color;
    with R do
      Canvas.Polygon([Point(Left, Top),
        Point(Right - 1, Top),
        Point((Right - 1 + Left) div 2, Bottom - 1)]);
  end;
end;

procedure TTeThemeHighlight.DrawButton(Canvas: TCanvas; Width, Height: integer;
  State: TTeThemeButtonState);
var
  R: TRect;
  BackColor: TKColor;
  RColor, LRColor, SColor, LSColor: TKColor;
begin
  R := Rect(0, 0, Width, Height);
  BackColor := GetColor(ngcBtnFace);
  { Set BackColor }
  if State = ngsDisabled then
    BackColor := GetColor(ngcWindow)
  else
    if State = ngsPressed then
      BackColor := GetColor(ngcHighlight)
    else
      if State = ngsHot then
        BackColor := GetColor(ngcHotHighlight)
      else
        if (State in [ngsFocused, ngsDefault]) then
          BackColor := GetColor(ngcHighlight);

  { Calc Colors }
  RColor := RaisedColor(BackColor, 30);
  LRColor := RaisedColor(BackColor, 10);
  SColor := SunkenColor(BackColor, 30);
  LSColor := SunkenColor(BackColor, 10);
  { Draw Border }
  if State <> ngsDisabled then
  begin
    { Enabled }
    DrawRoundRect(Canvas, Rect(0, 0, Width, Height), 3, GetColor(ngcBorder));
    InflateRect(R, -1, -1);
    MoveTo(Canvas, R.Right-1, R.Bottom-1);
    LineTo(Canvas, R.Right, R.Bottom, GetColor(ngcBorder));
    { Draw State }
    if State = ngsPressed then
    begin
      DrawEdge(Canvas, R, SColor, RColor);
      InflateRect(R, -1, -1);
      FillRect(Canvas, R, BackColor);
    end
    else
    begin
      DrawLine(Canvas, Rect(1, R.Top, R.Right-1, R.Top), RColor);
      DrawLine(Canvas, Rect(R.Left, 1, R.Left, R.Bottom-1), RColor);

      DrawLine(Canvas, Rect(1, R.Bottom-1, R.Right-1, R.Bottom-1), SColor);
      DrawLine(Canvas, Rect(R.Right-1, 1, R.Right-1, R.Bottom-1), SColor);

      InflateRect(R, -1, -1);
      FillRect(Canvas, R, BackColor);
      DrawEdge(Canvas, R, LRColor, LSColor);
    end;
  end
  else
  begin
    { Disabled }
    DrawRoundRect(Canvas, Rect(0, 0, Width, Height), 3, GetColor(ngcDisabled));
  end;
end;

procedure TTeThemeHighlight.DrawCheckBox(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  BackColor, CheckColor, RColor, SColor: TKColor;
  B: TTeBitmap;
  i, j: integer;
begin
  { Select Color}
  if ButtonState <> ngsDisabled then
  begin
    { Enabled }
    if ButtonState = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      if ButtonState = ngsFocused then
        BackColor := GetColor(ngcHighlight)
      else
        BackColor := GetColor(ngcWindow);

    RColor := RaisedColor(BackColor, 50);
    SColor := SunkenColor(BackColor, 50);
    CheckColor := GetColor(ngcBorder);
    DrawRect(Canvas, Rect, CheckColor);
  end
  else
  begin
    { Disables }
    RColor := GetColor(ngcWindow);
    SColor := GetColor(ngcWindow);
    CheckColor := GetColor(ngcDisabled);
    DrawRect(Canvas, Rect, CheckColor);
  end;

  { Draw Border }
  InflateRect(Rect, -1, -1);
  DrawEdge(Canvas, Rect, SColor, RColor);
  InflateRect(Rect, -1, -1);

  FillRect(Canvas, Rect, GetColor(ngcWindow));

  { Draw }
  case CheckState of
    ngcChecked: begin
      { Draw Check }
      B := TTeBitmap.Create;
      try
        B.SetSize(GetMetrix(ngmCheckBoxWidth), GetMetrix(ngmCheckBoxHeight));
        B.Clear(ckWhite);
        { Draw to B}
        DrawFrameControl(B.DC, Classes.Rect(0, 0, B.Width, B.Height), DFC_BUTTON, DFCS_BUTTONCHECK or DFCS_CHECKED);
        { Draw to Buffer }
        for i := 2 to B.Width-3 do
          for j := 2 to B.Height-3 do
            if (TKColorRec(B.Pixels[i, j]).R = 0) and
               (TKColorRec(B.Pixels[i, j]).G = 0) and
               (TKColorRec(B.Pixels[i, j]).B = 0)
            then
              Canvas.Pixels[Rect.Left + i - 2, Rect.Top + j - 2] := CheckColor;
      finally
        B.Free;
      end;
    end;
    ngcMixed: begin
      { Draw Grayed }
      InflateRect(Rect, -1, -1);
      FillRect(Canvas, Rect, CheckColor);
    end;
  end;
end;

procedure TTeThemeHighlight.DrawRadioButton(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  BackColor, CheckColor, SColor: TKColor;
begin
  { Draw radio button }

  { Select Color}
  if ButtonState <> ngsDisabled then
  begin
    { Enabled }
    if ButtonState = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      if ButtonState = ngsFocused then
        BackColor := GetColor(ngcHighlight)
      else
        BackColor := GetColor(ngcWindow);

    SColor := SunkenColor(BackColor, 30);
    CheckColor := GetColor(ngcBorder);
    DrawRoundRect(Canvas, Rect, 6, CheckColor);
  end
  else
  begin
    { Disables }
    SColor := GetColor(ngcWindow);
    CheckColor := GetColor(ngcDisabled);
    DrawRoundRect(Canvas, Rect, 6, CheckColor);
  end;

  { Draw Border }
  InflateRect(Rect, -1, -1);
  DrawRoundRect(Canvas, Rect, 2, SColor);
  DrawRoundRect(Canvas, Rect, 6, SColor);
  InflateRect(Rect, -1, -1);

  FillRoundRect(Canvas,  Rect, 2, GetColor(ngcWindow));

  { Draw }
  if CheckState = ngcChecked then
  begin
    { Draw Check }
    InflateRect(Rect, -1, -1);
    FillRoundRect(Canvas,  Rect, 2, CheckColor);
  end;
end;

procedure TTeThemeHighlight.DrawTrackBarThumb(Canvas: TCanvas; R: TRect;
  Orientation: TTrackOrientation; TickMarks: TTickMark;
  State: TTeThemeButtonState);
var
  BackColor: TKColor;
  SColor, RColor: TKColor;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State in [ngsPressed, ngsFocused] then
      BackColor := GetColor(ngcHighlight)
    else
      if State = ngsHot then
        BackColor := GetColor(ngcHotHighlight)
      else
        BackColor := GetColor(ngcBtnFace);

    if State = ngsPressed then
    begin
      SColor := RaisedColor(BackColor, 30);
      RColor := SunkenColor(BackColor, 30);
    end
    else
    begin
      RColor := RaisedColor(BackColor, 30);
      SColor := SunkenColor(BackColor, 30);
    end;

    if Orientation = toHorizontal then
    begin
      case TickMarks of
        tmBottomRight: begin
          FillPolygon(Canvas, [Point(R.Left, R.Top),
            Point(R.Right - 1, R.Top),
            Point(R.Right - 1, R.Bottom - RectWidth(R) div 2),
            Point(R.Left + RectWidth(R) div 2, R.Bottom),
            Point(R.Left, R.Bottom - RectWidth(R) div 2)],
            BackColor);

          MoveTo(Canvas, R.Left + 1, R.Bottom - RectWidth(R) div 2);
          LineTo(Canvas, R.Left + 1, R.Top + 1, RColor);
          LineTo(Canvas, R.Right - 1, R.Top + 1, RColor);

          MoveTo(Canvas, R.Right - 2, R.Top + 1);
          LineTo(Canvas, R.Right - 2, R.Bottom - RectWidth(R) div 2, SColor);

          DrawPolygon(Canvas, [Point(R.Left, R.Top),
            Point(R.Right - 1, R.Top),
            Point(R.Right - 1, R.Bottom - RectWidth(R) div 2),
            Point(R.Left + RectWidth(R) div 2, R.Bottom),
            Point(R.Left, R.Bottom - RectWidth(R) div 2)],
            GetColor(ngcBorder));
        end;
        tmTopLeft: begin
          FillPolygon(Canvas, [Point(R.Left, R.Bottom),
            Point(R.Right - 1, R.Bottom),
            Point(R.Right - 1, R.Top + RectWidth(R) div 2),
            Point(R.Left + RectWidth(R) div 2, R.Top),
            Point(R.Left, R.Top + RectWidth(R) div 2)],
            BackColor);

          MoveTo(Canvas, R.Left + 1, R.Top + RectWidth(R) div 2);
          LineTo(Canvas, R.Left + 1, R.Bottom - 1, RColor);

          MoveTo(Canvas, R.Left + 1, R.Bottom - 1);
          LineTo(Canvas, R.Right - 2, R.Bottom - 1, SColor);
          LineTo(Canvas, R.Right - 2, R.Top + RectWidth(R) div 2, SColor);

          DrawPolygon(Canvas, [Point(R.Left, R.Bottom),
            Point(R.Right - 1, R.Bottom),
            Point(R.Right - 1, R.Top + RectWidth(R) div 2),
            Point(R.Left + RectWidth(R) div 2, R.Top),
            Point(R.Left, R.Top + RectWidth(R) div 2)],
            GetColor(ngcBorder));
        end;
        tmBoth: begin
          DrawRoundRect(Canvas, R, 3, ckBorder);
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, BackColor);
          DrawEdge(Canvas, R, RColor, SColor);
        end;
      end;
    end
    else
    begin
      { Orientation = toVertical }
      case TickMarks of
        tmBottomRight: begin
          FillPolygon(Canvas, [Point(R.Left, R.Top),
            Point(R.Right - RectHeight(R) div 2, R.Top),
            Point(R.Right, R.Top + RectHeight(R) div 2),
            Point(R.Right - RectHeight(R) div 2, R.Bottom -1),
            Point(R.Left, R.Bottom - 1)],
            BackColor);

          MoveTo(Canvas, R.Left + 1, R.Bottom - 1);
          LineTo(Canvas, R.Left + 1, R.Top + 1, RColor);
          LineTo(Canvas, R.Right - RectHeight(R) div 2, R.Top + 1, RColor);

          MoveTo(Canvas, R.Left + 1, R.Bottom - 2);
          LineTo(Canvas, R.Right - RectHeight(R) div 2, R.Bottom - 2, SColor);

          DrawPolygon(Canvas, [Point(R.Left, R.Top),
            Point(R.Right - RectHeight(R) div 2, R.Top),
            Point(R.Right, R.Top + RectHeight(R) div 2),
            Point(R.Right - RectHeight(R) div 2, R.Bottom -1),
            Point(R.Left, R.Bottom - 1)],
            GetColor(ngcBorder));
        end;
        tmTopLeft: begin
          FillPolygon(Canvas, [Point(R.Left + RectHeight(R) div 2, R.Top),
            Point(R.Right, R.Top),
            Point(R.Right, R.Bottom - 1),
            Point(R.Left + RectHeight(R) div 2, R.Bottom - 1),
            Point(R.Left, R.Bottom - RectHeight(R) div 2 - 1)],
            BackColor);

          MoveTo(Canvas, R.Left + RectHeight(R) div 2, R.Top + 1);
          LineTo(Canvas, R.Right - 1, R.Top + 1, RColor);

          MoveTo(Canvas, R.Left + RectHeight(R) div 2, R.Bottom - 2);
          LineTo(Canvas, R.Right - 1, R.Bottom - 2, SColor);
          LineTo(Canvas, R.Right - 1, R.Top + 1, SColor);

          DrawPolygon(Canvas, [Point(R.Left + RectHeight(R) div 2, R.Top),
            Point(R.Right, R.Top),
            Point(R.Right, R.Bottom - 1),
            Point(R.Left + RectHeight(R) div 2, R.Bottom - 1),
            Point(R.Left, R.Bottom - RectHeight(R) div 2 - 1)],
            GetColor(ngcBorder));
        end;
        tmBoth: begin
          DrawRoundRect(Canvas, R, 3, ckBorder);
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, BackColor);
          DrawEdge(Canvas, R, RColor, SColor);
        end;
      end;
    end;
end;

procedure TTeThemeHighlight.DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect);
begin
  FillRect(Canvas, R, GetColor(ngcBtnFace));
  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));

  if not IsRectEmpty(HighlightR) then
  begin
    InflateRect(HighlightR, -1, -1);
    FillRect(Canvas, HighlightR, GetColor(ngcHighlight));
  end;
end;

procedure TTeThemeHighlight.DrawProgessFrame(Canvas: TCanvas; R: TRect);
var
  BackColor: TKColor;
  BorderColor: TKColor;
begin
  BackColor := GetColor(ngcWindow);
  BorderColor := GetColor(ngcBorder);

  DrawRoundRect(Canvas, R, 3, BorderColor);
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, BackColor);
end;

procedure TTeThemeHighlight.DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
  Orientation: TTeBarOrientation; Smooth: boolean);
const
  SegRation   = 0.64; { Segment width to height ration }
  SegSpacing  = 2;    { Spacing b/w segment }
var
  i: integer;
  SegW, SegH, FillSegCount: integer;
  FillColor: TKColor;
  FillR: TRect;
begin
  { Draw Bar }
  FillColor := GetColor(ngcHighlight);

  if Orientation = kboHorizontal then
  begin
    { Horizontal }
    if Smooth then
    begin
      { Smooth }
      FillRect(Canvas, BarR, FillColor);
    end
    else
    begin
      { Segments }
      SegH := RectHeight(R);
      SegW := Round(SegH * SegRation) + SegSpacing;
      { Calc SegCount }
      FillSegCount := RectWidth(BarR) div SegW;
      { Draw Segments }
      if FillSegCount > 0 then
        for i := 0 to FillSegCount do
        begin
          FillR := R;
          FillR.Right := FillR.Left + SegW - SegSpacing;
          OffsetRect(FillR, i * SegW, 0);
          { Clip }
          IntersectRect(FillR, FillR, R);
          { Draw }
          FillRect(Canvas, FillR, FillColor);
        end;
    end;
  end
  else
  begin
    { Vertical }
    if Smooth then
    begin
      { Smooth }
      FillRect(Canvas, BarR, FillColor);
    end
    else
    begin
      { Segments }
      SegH := RectWidth(R);
      SegW := Round(SegH * SegRation) + SegSpacing;
      { Calc SegCount }
      FillSegCount := RectHeight(BarR) div SegW;
      { Draw Segments }
      if FillSegCount > 0 then
        for i := 0 to FillSegCount do
        begin
          FillR := R;
          FillR.Top := FillR.Bottom - SegW + SegSpacing;
          OffsetRect(FillR, 0, - (i * SegW));
          { Clip }
          IntersectRect(FillR, FillR, R);
          { Draw }
          FillRect(Canvas, FillR, FillColor);
        end;
    end;
  end;
end;

procedure TTeThemeHighlight.DrawPanel(Canvas: TCanvas; R: TRect; ShowBevel, ShowCaption: boolean);
begin
  { Draw Panel Face }
  FillRoundRect(Canvas,  R, 3, GetColor(ngcWindow));
  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
end;

procedure TTeThemeHighlight.DrawPanelCaption(Canvas: TCanvas; R: TRect; Caption: WideString);
begin
  { Draw line }
  MoveTo(Canvas, R.Left, R.Bottom);
  LineTo(Canvas, R.Right, R.Bottom, GetColor(ngcBorder));

  { Draw Caption  }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));

  { Draw Text }
  InflateRect(R, -5, 0);
  Canvas.Font.Color := GetColor(ngcWindowText);
  Canvas.Font.Name := GetFontName(ngfWindowText);
  Canvas.Font.Size := GetFontSize(ngfWindowText);
  Canvas.Font.Style:= GetFontStyle(ngfWindowText);
  DrawText(Canvas, Caption, R, DrawTextBiDiModeFlags(DT_LEFT or DT_SINGLELINE or DT_VCenter));
end;

procedure TTeThemeHighlight.DrawPanelButton(Canvas: TCanvas; R: TRect;
  Kind: TTePanelButtonKind; State: TTeThemeButtonState; Rolled: boolean);
var
  BackColor: TKColor;
  KindFlags, Flags: integer;
  B: TTeBitmap;
  P: TKColorRec;
  i, j: integer;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State in [ngsPressed, ngsFocused] then
      BackColor := GetColor(ngcHighlight)
    else
      if State = ngsHot then
        BackColor := GetColor(ngcHotHighlight)
      else
        BackColor := GetColor(ngcBtnFace);

  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, BackColor);
  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    DrawEdge(Canvas, R, RaisedColor(BackColor, 30), SunkenColor(BackColor, 30));

  { Draw Glyph }
  if Kind = pbkHide then
  begin
    KindFlags := DFC_CAPTION;
    Flags := DFCS_CAPTIONCLOSE
  end
  else
  begin
    KindFlags := DFC_SCROLL;
    if Rolled then
      Flags := DFCS_SCROLLDOWN
    else
      Flags := DFCS_SCROLLUP;
  end;

  InflateRect(R, 1, 1);
  B := TTeBitmap.Create;
  try
    B.SetSize(RectWidth(R), RectHeight(R));

    if State = ngsPressed then
      DrawFrameControl(B.DC, Rect(0, 0, B.Width, B.Height), KindFlags, Flags or DFCS_PUSHED)
    else
      DrawFrameControl(B.DC, Rect(0, 0, B.Width, B.Height), KindFlags, Flags);

    { Draw to Buffer }
    for i := 2 to B.Width - 3 do
      for j := 2 to B.Height - 3 do
      begin
        P := TKColorRec(B.Pixels[i, j]);
        if (P.R = 0) and (P.G = 0) and (P.B = 0) then
          Canvas.Pixels[R.Left + i, R.Top + j] := GetColor(ngcBorder);
      end;
  finally
    B.Free;
  end;
end;

procedure TTeThemeHighlight.DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect;
  Caption: WideString);
begin
  { Draw Frame }
  MoveTo(Canvas, R.Left + 2, R.Top);
  LineTo(Canvas, CaptionRect.Left, R.Top, GetColor(ngcBorder));
  MoveTo(Canvas, CaptionRect.Right, R.Top);
  LineTo(Canvas, R.Right - 2, R.Top, GetColor(ngcBorder));
  LineTo(Canvas, R.Right, R.Top + 2, GetColor(ngcBorder));
  LineTo(Canvas, R.Right, R.Bottom - 2, GetColor(ngcBorder));
  LineTo(Canvas, R.Right - 2, R.Bottom, GetColor(ngcBorder));
  LineTo(Canvas, R.Left + 2, R.Bottom, GetColor(ngcBorder));
  LineTo(Canvas, R.Left, R.Bottom - 2, GetColor(ngcBorder));
  LineTo(Canvas, R.Left, R.Top + 2, GetColor(ngcBorder));
  LineTo(Canvas, R.Left + 2, R.Top, GetColor(ngcBorder));

  { Draw Caption }
  DrawText(Canvas, Caption, CaptionRect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;


procedure TTeThemeHighlight.DrawScrollBar(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind);
begin
  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
end;

procedure TTeThemeHighlight.DrawScrollBarButton(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; LeftTop: boolean; State: TTeThemeButtonState);
var
  Flags: UINT;
  BackColor: TKColor;
begin
  if LeftTop then
    if Kind = sbHorizontal then
      Flags := DFCS_SCROLLLEFT
    else
      Flags := DFCS_SCROLLUP
  else
    if Kind = sbHorizontal then
      Flags := DFCS_SCROLLRIGHT
    else
      Flags := DFCS_SCROLLDOWN;

  if State = ngsPressed then
    Flags := Flags or DFCS_PUSHED or DFCS_FLAT;

  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State in [ngsPressed, ngsFocused] then
      BackColor := GetColor(ngcHighlight)
    else
      if State = ngsHot then
        BackColor := GetColor(ngcHotHighlight)
      else
        BackColor := GetColor(ngcBtnFace);

  FillRect(Canvas, R, BackColor);
  DrawFrameControlGlyph(Canvas, R, DFC_SCROLL, Flags, clBlack);

  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    DrawEdge(Canvas, R, RaisedColor(BackColor, 30), SunkenColor(BackColor, 30));
end;

procedure TTeThemeHighlight.DrawScrollBarSlider(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; State: TTeThemeButtonState);
var
  BackColor: TKColor;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State in [ngsPressed, ngsFocused] then
      BackColor := GetColor(ngcHighlight)
    else
      if State = ngsHot then
        BackColor := GetColor(ngcHotHighlight)
      else
        BackColor := GetColor(ngcBtnFace);

  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  FillRect(Canvas, R, BackColor);

  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    DrawEdge(Canvas, R, RaisedColor(BackColor, 30), SunkenColor(BackColor, 30));
end;

procedure TTeThemeHighlight.DrawTabBorder(Canvas: TCanvas; R: TRect);
begin
  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 15), SunkenColor(GetColor(ngcBtnFace), 15));

  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeHighlight.DrawTab(Canvas: TCanvas; R: TRect; TabPosition: TTabPosition; State: TTeThemeButtonState);
var
  Color: TKColor;
begin
  if State = ngsHot then
    Color := GetColor(ngcHotHighlight)
  else
    if State = ngsFocused then
      Color := GetColor(ngcHighlight)
    else
      Color := GetColor(ngcBtnFace);

  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, RaisedColor(Color, 30), SunkenColor(Color, 30));

  InflateRect(R, -1, -1);
  FillRect(Canvas, R, Color);

  InflateRect(R, 2, 2);

  case TabPosition of
    tpTop: R.Top := R.Bottom - 2;
    tpBottom: R.Bottom := R.Top + 2;
    tpLeft: R.Left := R.Right - 2;
    tpRight: R.Right := R.Left + 2;
  end;
  FillRect(Canvas, R, Color);
end;

procedure TTeThemeHighlight.DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
  TabPosition: TTabPosition; State: TTeThemeButtonState);
begin
  DrawScrollBarButton(Canvas, R, sbHorizontal, LeftTop, State);
end;

procedure TTeThemeHighlight.DrawControlFrame(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState);
var
  Color, BColor: TColor;
begin
  if State <> ngsDisabled then
  begin
    Color := GetColor(ngcWindow);
    BColor := GetColor(ngcBorder);
  end
  else
  begin
    Color := GetColor(ngcDisabled);
    BColor := GetColor(ngcDisabledBorder);
  end;

  FillRect(Canvas, Rect(R.Right - 20, R.Bottom - 20, R.Right, R.Bottom), Color);
  
  DrawRect(Canvas, R, BColor);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
end;

procedure TTeThemeHighlight.DrawComboButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState);
var
  BackColor: TKColor;
  GlyphR: TRect;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State in [ngsPressed, ngsFocused] then
      BackColor := GetColor(ngcHighlight)
    else
      if State = ngsHot then
        BackColor := GetColor(ngcHotHighlight)
      else
        BackColor := GetColor(ngcBtnFace);

  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  FillRect(Canvas, R, BackColor);

  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    DrawEdge(Canvas, R, RaisedColor(BackColor, 30), SunkenColor(BackColor, 30));

  { Draw Glyph }
  GlyphR := Rect(0, 0, 7, 4);
  RectCenter(GlyphR, R);
  OffsetRect(GlyphR, 0, 1);

  if State = ngsPressed then
    OffsetRect(GlyphR, 1, 1);

  with GlyphR do
  begin
    FillPolygon(Canvas, [Point(Left, Top), Point(Right - 1, Top),
      Point((Right - 1 + Left) div 2, Bottom - 1)], GetColor(ngcBorder));
  end;
end;

procedure TTeThemeHighlight.DrawSpeedButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
begin
  if Flat and (State = ngsNormal) and not Exclusive then Exit;
  if State = ngsDisabled then Exit;

  if not Flat then
  begin
    { Standard }
    case State of
      ngsNormal:
        if not Exclusive then
        begin
          FillRoundRect(Canvas, R, 3, GetColor(ngcBtnFace));
          DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
          InflateRect(R, -1, -1);
          DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));
        end
        else
        begin
          FillRoundRect(Canvas, R, 3, GetColor(ngcHighlight));
          DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
        end;
      ngsPressed:
        if not Exclusive then
        begin
          FillRoundRect(Canvas, R, 3, GetColor(ngcBtnFace));
          DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
          InflateRect(R, -1, -1);
          DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), 30), RaisedColor(GetColor(ngcBtnFace), 30));
        end
        else
        begin
          FillRoundRect(Canvas, R, 3, GetColor(ngcHighlight));
          DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
        end;
      ngsDisabled:
        begin
          FillRoundRect(Canvas, R, 3, GetColor(ngcBtnFace));
          DrawRoundRect(Canvas, R, 3, GetColor(ngcDisabled));
        end;
    end;
  end
  else
  begin
    { Flat }
    if State = ngsDisabled then
    begin
      FillRoundRect(Canvas, R, 3, GetColor(ngcWindow));
      DrawRoundRect(Canvas, R, 3, GetColor(ngcDisabled));
    end
    else
    begin
      if State = ngsPressed then
      begin
        FillRoundRect(Canvas, R, 3, GetColor(ngcBtnFace));
        DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
        InflateRect(R, -1, -1);
        DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), 30), RaisedColor(GetColor(ngcBtnFace), 30));
      end
      else
        if State = ngsHot then
          if not Exclusive then
          begin
            FillRoundRect(Canvas, R, 3, GetColor(ngcHotHighlight));
            DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
            InflateRect(R, -1, -1);
            DrawEdge(Canvas, R, RaisedColor(GetColor(ngcHotHighlight), 30), SunkenColor(GetColor(ngcHotHighlight), 30));
          end
          else
          begin
            FillRoundRect(Canvas, R, 3, GetColor(ngcHotHighlight));
            DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
            InflateRect(R, -1, -1);
            DrawEdge(Canvas, R, RaisedColor(GetColor(ngcHotHighlight), 30), SunkenColor(GetColor(ngcHotHighlight), 30));
          end;
    end;

    if Exclusive and (State = ngsNormal) then
    begin
      FillRoundRect(Canvas, R, 3, GetColor(ngcHighlight));
      DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
    end;
  end;
end;

procedure TTeThemeHighlight.DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
var
  GlyphR: TRect;
begin
  DrawSpeedButton(Canvas, R, State, Flat, Exclusive);
  { Draw Glyph }
  if State = ngsPressed then
    OffsetRect(R, 1, 1);

  GlyphR := Rect(0, 0, 7, 4);
  RectCenter(GlyphR, R);

  with GlyphR do
    if State <> ngsDisabled then
    begin
      FillPolygon(Canvas, [Point(Left, Top),
        Point(Right - 1, Top),
        Point((Right - 1 + Left) div 2, Bottom - 1)],
        GetColor(ngcBorder));
    end
    else
    begin
      MoveTo(Canvas, (Right + Left) div 2, Bottom - 1);
      LineTo(Canvas, Left, Top, GetColor(ngcDisabled));
      LineTo(Canvas, Right, Top, GetColor(ngcDisabled));
      MoveTo(Canvas, Right, Top);
      LineTo(Canvas, (Right + Left) div 2 + 1 - 1, Bottom + 1 - 1, GetColor(ngcBorder));
    end;
end;

procedure TTeThemeHighlight.DrawSpinButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Up: boolean);
begin
  if State = ngsNormal then
    DrawSpeedButton(Canvas, R, State, false, false)
  else
    DrawSpeedButton(Canvas, R, State, true, false);
  { Draw Glyph }
end;

procedure TTeThemeHighlight.DrawControlBar(Canvas: TCanvas; R: TRect);
begin
  { ControlBar Background }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeHighlight.DrawControlBarFrame(Canvas: TCanvas; R,
  GrabberRect: TRect);
var
  Color: TKColor;
begin
  { ControlBar Frame }
  DrawRoundRect(Canvas, R, 3, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcBtnFace));
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 50),
    SunkenColor(GetColor(ngcBtnFace), 50));

  { Draw Grabber }
  Color := GetColor(ngcBtnFace);

  InflateRect(GrabberRect, -3, -1);
  DrawRoundRect(Canvas, GrabberRect, 3, GetColor(ngcBorder));

  InflateRect(GrabberRect, -1, -1);
  FillRect(Canvas, GrabberRect, Color);
  DrawEdge(Canvas, GrabberRect, RaisedColor(Color, 50), SunkenColor(Color, 50));
end;

procedure TTeThemeHighlight.DrawToolbar(Canvas: TCanvas; R: TRect);
begin
  { Draw toolbar }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeHighlight.DrawGridCell(Canvas: TCanvas; R: TRect;
  State: TGridDrawState);
var
  Color: TColor;
begin
  { Draw grid cell }
  if gdFixed in State then
    Color := GetColor(ngcBtnFace)
  else
    if gdFocused in State then
      Color := GetColor(ngcHighlight)
    else
      if gdSelected in State then
        Color := GetColor(ngcHotHighlight)
      else
        Color := GetColor(ngcWindow);

  FillRect(Canvas, R, Color);

  if gdFixed in State then
  begin
    DrawEdge(Canvas, R, RaisedColor(Color, 30), SunkenColor(Color, 30));
  end;
end;

procedure TTeThemeHighlight.DrawSplitter(Canvas: TCanvas; ARect: TRect; AHot, ABeveled: boolean);
begin
  if ABeveled then
  begin
    DrawRoundRect(Canvas, ARect, 3, GetColor(ngcBorder));
    InflateRect(ARect, -1, -1);
  end;

  if not AHot then
  begin
    FillRect(Canvas, ARect, GetColor(ngcWindow))
  end
  else
  begin
    DrawEdge(Canvas, ARect, RaisedColor(GetColor(ngcHighlight), 30), SunkenColor(GetColor(ngcHighlight), 30));
    InflateRect(ARect, -1, -1);
    FillRect(Canvas, ARect, GetColor(ngcHotHighlight));
  end;
end;

procedure TTeThemeHighlight.DrawHeaderSection(Canvas: TCanvas; ARect: TRect;
  Section: TTeHeaderSection; AState: TTeSectionState);
var
  Color, RColor, SColor: TColor;
begin
  { Section may be nil}
   
  if AState = ssDraggedOut then
    Color := clBlack
  else
    if AState = ssUnderDrag then
      Color := GetColor(ngcHighlight)
    else
      if AState in [ssPressed] then
        Color := GetColor(ngcHighlight)
      else
        if AState in [ssHot, ssDraggin] then
          Color := GetColor(ngcHotHighlight)
        else
          Color := GetColor(ngcBtnFace);

  DrawRoundRect(Canvas, ARect, 3, GetColor(ngcBorder));
  InflateRect(ARect, -1, -1);
  FillRect(Canvas, ARect, Color);

  { Draw bevel }
  RColor := RaisedColor(Color, 30);
  SColor := SunkenColor(Color, 30);

  if AState in [ssPressed, ssUnderDrag] then
    DrawEdge(Canvas, ARect, SColor, RColor)
  else
    DrawEdge(Canvas, ARect, RColor, SColor)
end;

procedure TTeThemeHighlight.DrawStatusBar(Canvas: TCanvas; ARect: TRect);
begin
  DrawRoundRect(Canvas, ARect, 3, GetColor(ngcBorder));
  InflateRect(ARect, -1, -1);
  DrawEdge(Canvas, ARect, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));
  InflateRect(ARect, -1, -1);
  FillRect(Canvas, ARect, GetColor(ngcBtnFace));
end;

procedure TTeThemeHighlight.DrawStatusGripper(Canvas: TCanvas; ARect: TRect);
begin
  with ARect do
  begin
    InflateRect(ARect, -4, -4);
    OffsetRect(ARect, 3, 3);
    
    FillPolygon(Canvas, [Point(Right, Top - 4), Point(Right, Top), Point(Left, Bottom), Point(Left - 4, Bottom)], GetColor(ngcCaption));
    DrawPolygon(Canvas, [Point(Right, Top - 4), Point(Right, Top), Point(Left, Bottom), Point(Left - 4, Bottom)], GetColor(ngcBorder));

    FillPolygon(Canvas, [Point(Right, Top + 3), Point(Right, Bottom), Point(Left + 3, Bottom)], GetColor(ngcCaption));
    DrawPolygon(Canvas, [Point(Right, Top + 3), Point(Right, Bottom), Point(Left + 3, Bottom)], GetColor(ngcBorder));
   end;
end;

procedure TTeThemeHighlight.DrawStatusPanel(Canvas: TCanvas; ARect: TRect;
  Panel: TTeStatusPanel);
begin
  DrawEdge(Canvas, ARect, SunkenColor(GetColor(ngcBtnFace), 30), RaisedColor(GetColor(ngcBtnFace), 30));
  InflateRect(ARect, -1, -1);
  DrawRoundRect(Canvas, ARect, 3, GetColor(ngcBorder));
  InflateRect(ARect, -1, -1);
  FillRect(Canvas, ARect, GetColor(ngcBtnFace));
end;

procedure TTeThemeHighlight.DrawScrollBox(Canvas: TCanvas; R: TRect;
  Enabled: boolean);
var
  Color, BColor: TColor;
begin
  if Enabled then
  begin
    Color := GetColor(ngcBtnFace);
    BColor := GetColor(ngcBorder);
  end
  else
  begin
    Color := GetColor(ngcDisabled);
    BColor := GetColor(ngcDisabledBorder);
  end;

  FillRect(Canvas, Rect(R.Right - 20, R.Bottom - 20, R.Right, R.Bottom), Color);

  DrawRect(Canvas, R, BColor);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
end;

function TTeThemeHighlight.GetRegion(Width, Height: integer;
  BorderStyle: TTeBorderStyle): HRgn;
begin
  Result := CreateRoundRectRgn(0, 0, Width+1, Height+1, 3, 3);
end;

initialization
  RegisterTheme(TTeThemeHighlight);
end.




