{==============================================================================

  KDE Themes
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: tethemekde.pas,v 1.4 2002/10/28 21:04:02 Evgeny Exp $

===============================================================================}

unit tethemekde;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus,
  Grids, te_controls, KsThemeThemes;

const

  OuterValue = 120;
  InnerValue = 120;

type

{ TTeThemeKDE class }

  TTeThemeKDE = class(TTeTheme)
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

const

  RaisedValue = 60;
  SunkenValue = 60;

procedure DrawLine(Canvas: TCanvas; R: TRect; Color: TColor);
begin
  MoveTo(Canvas, R.Left, R.Top);
  LineTo(Canvas, R.Right, R.Bottom, Color);
end;

procedure FillHalfRect(Canvas: TCanvas; R: TRect; Color: TColor);
var
  RColor, SColor: TColor;
  i, j: integer;
  HalfBrush: TBrush;
  HalfBitmap: TBitmap;
begin
  if R.Left < 0 then R.Left := 0;
  if R.Top < 0 then R.Top := 0;
  if RectWidth(R) <= 0 then Exit;
  if RectHeight(R) <= 0 then Exit;

  RColor := RaisedColor(Color, RaisedValue);
  SColor := SunkenColor(Color, SunkenValue);

  HalfBrush := TBrush.Create;
  HalfBitmap := TBitmap.Create;
  HalfBitmap.Width := 8;
  HalfBitmap.Height := 8;

  { Create halftone bitmap }
  HalfBitmap.Canvas.Brush.Style := bsSolid;
  HalfBitmap.Canvas.Brush.Color := Color;
  HalfBitmap.Canvas.FillRect(Rect(0, 0, 8, 8));

  for i := 0 to HalfBitmap.Width - 1 do
    for j := 0 to HalfBitmap.Height - 1 do
    begin
      if (i mod 4 = 0) and (j mod 4 = 0) then
      begin
        HalfBitmap.Canvas.Pixels[i, j] := RColor;
        HalfBitmap.Canvas.Pixels[i + 1, j + 1] := SColor;
      end;
    end;

  HalfBrush.Bitmap := HalfBitmap;

  Canvas.Brush := HalfBrush;
  Canvas.FillRect(R);

  HalfBitmap.Free;
  HalfBrush.Free;
end;

{ TTeThemeKDE }

constructor TTeThemeKDE.Create;
begin
  inherited Create;
end;

destructor TTeThemeKDE.Destroy;
begin
  inherited Destroy;
end;

{ Metrix ======================================================================}

function TTeThemeKDE.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaption: Result := RGB(82, 110, 149);
    ngcInactiveCaption: Result := RGB(128, 128, 128);
    ngcCaptionShadow: Result := clBlack;
    ngcCaptionText: Result := clWhite;
    ngcInactiveCaptionText: Result := clWhite;
    ngcBorder: Result := clBlack;
    ngcWindow: Result := clWindow;
    ngcBtnFace: Result := clBtnFace;
    ngcHighlight: Result := clHighlight;
    ngcHotHighlight: Result := RGB(220, 220, 230);
    { Menus }
    ngcMenuBorder: Result := clBlack;
    ngcMenuBar: Result := KColorToColor(RaisedColor(KColor(clBtnFace), 10));
    ngcMenuBarHighlight: Result := clHighlight;
    ngcMenuItem: Result := KColorToColor(RaisedColor(KColor(clBtnFace), 10));
    ngcMenuItemHighlight: Result := clHighlight;
    ngcMenuBarText: Result := clMenuText;
    ngcMenuBarHighlightText: Result := clHighlightText;
    ngcMenuItemText: Result := clMenuText;
    ngcMenuItemHighlightText: Result := clHighlightText;
    ngcMenuItemDisabledText: Result := clGray;
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

function TTeThemeKDE.GetFontName(Font: TTeThemeFont): string;
begin
  case Font of
    ngfCaptionText: Result := 'Verdana';
    ngfSmCaptionText: Result := 'Verdana';
    { Menus }
    ngfMenuBarText: Result := 'Verdana';
    ngfMenuItemText: Result := 'Verdana';
    { Controls }
    ngfWindowText: Result := 'Verdana';
  else
    Result := 'Verdana';
  end;
end;

function TTeThemeKDE.GetFontSize(Font: TTeThemeFont): integer;
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

function TTeThemeKDE.GetFontStyle(Font: TTeThemeFont): TFontStyles;
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

function TTeThemeKDE.GetMetrix(Metrix: TTeThemeMetrix): integer;
begin
  case Metrix of
    ngmBorderWidth: Result := 3;
    ngmSmBorderWidth: Result := 3;
    ngmCaptionHeight: Result := 20;
    ngmSmCaptionHeight: Result := 20;
    ngmCaptionMargin: Result := 30;
    ngmButtonWidth: Result := 16;
    ngmButtonHeight: Result := 16;
    ngmSmButtonWidth: Result := 16;
    ngmSmButtonHeight: Result := 16;
    ngmButtonMarginX: Result := 3;
    ngmButtonMarginY: Result := 3;
    ngmButtonSpace: Result := 1;
    ngmSmButtonMarginX: Result := 3;
    ngmSmButtonMarginY: Result := 3;
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
    ngmTrackBarHeight: Result := 10;
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

class function TTeThemeKDE.GetThemeName: string;
begin
  Result := 'KDE';
end;

class function TTeThemeKDE.UseTheme: boolean;
begin
  Result := true;
end;

procedure TTeThemeKDE.ChangeThemeColors;
begin
end;

{ Form Draw ===================================================================}

procedure TTeThemeKDE.DrawSysButton(Canvas: TCanvas; R: TRect; Kind: TTeThemeSysButton;
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

  { Draw button back }
  InflateRect(BR, 1, 1);
  FillRect(Canvas, BR, GetColor(ngcBtnFace));

  BR := R;
  { change color }
  if Hot then
    Color := GetColor(ngcHotHighlight)
  else
    Color := GetColor(ngcBtnFace);

  FillRect(Canvas, BR, Color); 
  if Down then
  begin
    DrawEdge(Canvas, BR, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
    InflateRect(BR, -1, -1);
    DrawEdge(Canvas, BR, SunkenColor(Color, InnerValue), RaisedColor(Color, InnerValue));
  end
  else
  begin
    DrawEdge(Canvas, BR, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
    InflateRect(BR, -1, -1);
    DrawEdge(Canvas, BR, RaisedColor(Color, InnerValue), SunkenColor(Color, InnerValue));
  end;

  InflateRect(R, -1, -1);
  case Kind of
    ngbClose:
      begin
        DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONCLOSE or Flag, clBlack);
      end;
    ngbHelp: DrawFrameControlGlyph(Canvas, R, DFC_CAPTION, DFCS_CAPTIONHELP or Flag, clBlack);
    ngbMax:
      begin
        InflateRect(R, -3, -3);
        if Down then
          OffsetRect(R, 1, 1);
        DrawRect(Canvas, R, clBlack);
        InflateRect(R, -1, -1);
        DrawRect(Canvas, R, clBlack);
      end;
    ngbMin:
      begin
        BR := Rect(0, 0, 4, 4);
        RectCenter(BR, R);
        if Down then
          OffsetRect(BR, 1, 1);
        FillRect(Canvas, BR, clBlack);
      end;
    ngbRestore:
      begin
        InflateRect(R, -3, -3);
        if Down then
          OffsetRect(R, 1, 1);
        DrawRect(Canvas, R, clBlack);
        InflateRect(R, -1, -1);
        DrawRect(Canvas, R, clBlack);
      end;
    ngbRollUp, ngbRollDown:
      begin
        { Draw Glyph }
        InflateRect(R, -4, -5);
        Inc(R.Top);
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
        { Draw Glyph }
        InflateRect(R, -5, -5);
        OffsetRect(R, 3, 3);
        if Down then
          OffsetRect(R, 1, 1);

        FillRect(Canvas, R, clBlack);
      end;
  end;
end;

procedure TTeThemeKDE.DrawWindow(Canvas: TCanvas; Width,
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

  FillHalfRect(Canvas, CaptionRect, Color);

  { Draw Border }
  BorderRect := Rect(0, 0, Width, Height);
  DrawEdge(Canvas, BorderRect, RaisedColor(Color, RaisedValue), SunkenColor(Color, RaisedValue));
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, Color);
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, Color);
end;

{ Menus Draw ==================================================================}

procedure TTeThemeKDE.CalcMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; var AWidth, AHeight: integer);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

  AWidth := TextWidth(Canvas, Item.Caption) + 12;
  AHeight := GetMetrix(ngmMenuBarHeight);
end;

procedure TTeThemeKDE.CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
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

procedure TTeThemeKDE.DrawMenuBar(Canvas: TCanvas; Width,
  Height: integer);
begin
  FillRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcMenuBar));
end;

procedure TTeThemeKDE.DrawPopupMenu(Canvas: TCanvas; Width,
  Height: integer);
var
  R: TRect;
begin
  R := Rect(0, 0, Width, Height);
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcMenuBar), OuterValue), SunkenColor(GetColor(ngcMenuBar), OuterValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcMenuBar), InnerValue), SunkenColor(GetColor(ngcMenuBar), InnerValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, GetColor(ngcMenuBar), GetColor(ngcMenuBar));
end;

procedure TTeThemeKDE.DrawMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; Rect: TRect; Active, Hover: boolean);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

  InflateRect(Rect, -1, -1);
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
        FillRect(Canvas, Rect, GetColor(ngcHighlight))
      else
        FillRect(Canvas, Rect, GetColor(ngcMenuBarHighlight));
      Canvas.Font.Color := GetColor(ngcMenuBarHighlightText);
    end;
  end
  else
    Canvas.Font.Color := GetColor(ngcMenuBarText);

  { Text }
  DrawText(Canvas, Item.Caption, Rect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;

procedure TTeThemeKDE.DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
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

procedure TTeThemeKDE.DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
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

procedure TTeThemeKDE.DrawMenuScrollButton(Canvas: TCanvas;
  Rect: TRect; Button: TTeMenuScrollButton; Active: boolean);
var
  R: TRect;
begin
  Canvas.Font.Color := GetColor(ngcMenuItemText);

  if Active then
  begin
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));
    InflateRect(Rect, -1, -1);
    DrawEdge(Canvas, Rect, SunkenColor(GetColor(ngcMenuItem), InnerValue), RaisedColor(GetColor(ngcMenuItem), InnerValue));
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

procedure TTeThemeKDE.DrawButton(Canvas: TCanvas; Width, Height: integer;
  State: TTeThemeButtonState);
var
  R: TRect;
  BackColor: TKColor;
  RColor, LRColor, SColor, LSColor: TKColor;
begin
  R := Rect(0, 0, Width, Height);
  BackColor := GetColor(ngcBtnFace);
  { Set BackColor }
  if State = ngsHot then
    BackColor := GetColor(ngcHotHighlight);

  { Calc Colors }
  RColor := RaisedColor(BackColor, OuterValue);
  LRColor := RaisedColor(BackColor, InnerValue);
  SColor := SunkenColor(BackColor, OuterValue);
  LSColor := SunkenColor(BackColor, InnerValue);
  { Draw Border }
  if State <> ngsDisabled then
  begin
    { Enabled }
    if State = ngsPressed then
    begin
      DrawEdge(Canvas, R, SColor, RColor);
      InflateRect(R, -1, -1);
      DrawEdge(Canvas, R, LSColor, LRColor);
      InflateRect(R, -1, -1);
      FillRect(Canvas, R, BackColor);
    end
    else
    begin
      DrawEdge(Canvas, R, RColor, SColor);
      InflateRect(R, -1, -1);
      DrawEdge(Canvas, R, LRColor, LSColor);
      InflateRect(R, -1, -1);

      FillRect(Canvas, R, BackColor);
    end;
  end
  else
  begin
    { Disabled }
    DrawRect(Canvas, R, GetColor(ngcDisabled));
  end;
end;

procedure TTeThemeKDE.DrawCheckBox(Canvas: TCanvas; Rect: TRect;
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
      BackColor := GetColor(ngcWindow);

    CheckColor := GetColor(ngcBorder);
  end
  else
  begin
    { Disables }
    BackColor := GetColor(ngcBtnFace);
    CheckColor := GetColor(ngcDisabled);
  end;

  RColor := RaisedColor(GetColor(ngcBtnFace), InnerValue);
  SColor := SunkenColor(GetColor(ngcBtnFace), InnerValue);

  { Draw Border }
  DrawEdge(Canvas, Rect, SColor, RColor);
  InflateRect(Rect, -1, -1);
  DrawEdge(Canvas, Rect, SunkenColor(GetColor(ngcBtnFace), OuterValue), RaisedColor(GetColor(ngcBtnFace), OuterValue));
  InflateRect(Rect, -1, -1);

  FillRect(Canvas, Rect, BackColor);

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

procedure TTeThemeKDE.DrawRadioButton(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  BackColor, CheckColor: TKColor;
begin
  { Draw radio button }

  { Select Color}
  if ButtonState <> ngsDisabled then
  begin
    { Enabled }
    if ButtonState = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      BackColor := GetColor(ngcWindow);

    CheckColor := GetColor(ngcBorder);
  end
  else
  begin
    { Disables }
    BackColor := GetColor(ngcBtnFace);
    CheckColor := GetColor(ngcDisabled);
  end;

  { Draw Border }
  DrawRoundRect(Canvas, Rect, 6, SunkenColor(GetColor(ngcBtnFace), InnerValue));
  Inc(Rect.Left); Inc(Rect.Top);
  DrawRoundRect(Canvas, Rect, 6, RaisedColor(GetColor(ngcBtnFace), InnerValue));
  Dec(Rect.Right); Dec(Rect.Bottom);
  DrawRoundRect(Canvas, Rect, 2, SunkenColor(GetColor(ngcBtnFace), InnerValue));
  DrawRoundRect(Canvas, Rect, 6, SunkenColor(GetColor(ngcBtnFace), OuterValue));
  Inc(Rect.Left); Inc(Rect.Top);
  DrawRoundRect(Canvas, Rect, 2, RaisedColor(GetColor(ngcBtnFace), InnerValue));
  DrawRoundRect(Canvas, Rect, 6, GetColor(ngcBtnFace));
  Dec(Rect.Right); Dec(Rect.Bottom);

  { Fill }
  FillRoundRect(Canvas, Rect, 2, BackColor);

  { Draw }
  if CheckState = ngcChecked then
  begin
    { Draw Check }
    InflateRect(Rect, -2, -2);
    FillRoundRect(Canvas,  Rect, 2, CheckColor);
  end;
end;

procedure TTeThemeKDE.DrawTrackBarThumb(Canvas: TCanvas; R: TRect;
  Orientation: TTrackOrientation; TickMarks: TTickMark;
  State: TTeThemeButtonState);
var
  BackColor: TColor;
  LThumbHeight: integer;
  LThumbwidth: integer;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      BackColor := GetColor(ngcBtnFace);

    if Orientation = toHorizontal then
    begin
      LThumbHeight := RectHeight(R);
      LThumbWidth := RectWidth(R);

      case TickMarks of
        tmBottomRight: begin
          if State = ngsPressed then
            FillHalftonePolygon(Canvas, [Point(R.Left, R.Top),
              Point(R.Left + LThumbWidth - 1, R.Top),
              Point(R.Left + LThumbWidth - 1, R.Top + LThumbHeight - LThumbWidth div 2 - 1),
              Point(R.Left + LThumbWidth div 2, R.Top + LThumbHeight - 1),
              Point(R.Left, R.Top + LThumbHeight - LThumbWidth div 2 - 1)],
              BackColor, clWhite)
          else
            FillPolygon(Canvas, [Point(R.Left, R.Top),
              Point(R.Left + LThumbWidth - 1, R.Top),
              Point(R.Left + LThumbWidth - 1, R.Top + LThumbHeight - LThumbWidth div 2 - 1),
              Point(R.Left + LThumbWidth div 2, R.Top + LThumbHeight - 1),
              Point(R.Left, R.Top + LThumbHeight - LThumbWidth div 2 - 1)],
              BackColor);

          MoveTo(Canvas, R.Left + LThumbWidth - 2, R.Top);
          LineTo(Canvas, R.Left, R.Top, RaisedColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left, R.Top + LThumbHeight - LThumbWidth div 2 - 1, RaisedColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbWidth div 2, R.Top + LThumbHeight - 1, RaisedColor(BackColor, OuterValue));

          MoveTo(Canvas, R.Left + LThumbWidth - 2, R.Top + 1);
          LineTo(Canvas, R.Left + LThumbWidth - 2, R.Top + LThumbHeight - LThumbWidth div 2 - 1, SunkenColor(BackColor, InnerValue));
          LineTo(Canvas, R.Left + LThumbWidth div 2 - 1, R.Top + LThumbHeight - 1, SunkenColor(BackColor, InnerValue));

          MoveTo(Canvas, R.Left + LThumbWidth - 1, R.Top);
          LineTo(Canvas, R.Left + LThumbWidth - 1, R.Top + LThumbHeight - LThumbWidth div 2 - 1, SunkenColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbWidth div 2 - 1, R.Top + LThumbHeight, SunkenColor(BackColor, OuterValue));
        end;
        tmTopLeft: begin
          if State = ngsPressed then
            FillHalftonePolygon(Canvas, [Point(R.Left + LThumbWidth div 2, R.Top),
              Point(R.Left + LThumbWidth - 1, R.Top + LThumbWidth div 2),
              Point(R.Left + LThumbWidth - 1, R.Top + LThumbHeight - 1),
              Point(R.Left, R.Top + LThumbHeight - 1),
              Point(R.Left, R.Top + LThumbWidth div 2)],
              BackColor, clWhite)
          else
            FillPolygon(Canvas, [Point(R.Left + LThumbWidth div 2, R.Top),
              Point(R.Left + LThumbWidth - 1, R.Top + LThumbWidth div 2),
              Point(R.Left + LThumbWidth - 1, R.Top + LThumbHeight - 1),
              Point(R.Left, R.Top + LThumbHeight - 1),
              Point(R.Left, R.Top + LThumbWidth div 2)],
              BackColor);

          MoveTo(Canvas, R.Left, R.Top + LThumbHeight - 2);
          LineTo(Canvas, R.Left, R.Top + LThumbWidth div 2, RaisedColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbWidth div 2, R.Top, RaisedColor(BackColor, OuterValue));

          MoveTo(Canvas, R.Left + LThumbWidth div 2, R.Top + 1);
          LineTo(Canvas, R.Left + LThumbWidth - 2, R.Top + LThumbWidth div 2, SunkenColor(BackColor, InnerValue));
          LineTo(Canvas, R.Left + LThumbWidth - 2, R.Top + LThumbHeight - 2, SunkenColor(BackColor, InnerValue));
          LineTo(Canvas, R.Left, R.Top + LThumbHeight - 2, SunkenColor(BackColor, InnerValue));

          MoveTo(Canvas, R.Left + LThumbWidth div 2, R.Top);
          LineTo(Canvas, R.Left + LThumbWidth - 1, R.Top + LThumbWidth div 2, SunkenColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbWidth - 1, R.Top + LThumbHeight - 1, SunkenColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left - 1, R.Top + LThumbHeight - 1, SunkenColor(BackColor, OuterValue));
        end;
        tmBoth: begin
          if State = ngsPressed then
            FillHalftoneRect(Canvas, Rect(R.Left, R.Top, R.Left + LThumbWidth, R.Top + LThumbHeight), BackColor, clWhite)
          else
            FillRect(Canvas, Rect(R.Left, R.Top, R.Left + LThumbWidth, R.Top + LThumbHeight), BackColor);
          DrawEdge(Canvas, Rect(R.Left, R.Top, R.Left + LThumbWidth, R.Top + LThumbHeight),
            RaisedColor(BackColor, OuterValue), clBlack);
          DrawEdge(Canvas, Rect(R.Left + 1, R.Top + 1, R.Left + LThumbWidth - 1, R.Top + LThumbHeight - 1),
            BackColor, SunkenColor(BackColor, InnerValue));
        end;
      end;
    end
    else
    begin
      { Orientation = toVertical }
      LThumbHeight := RectWidth(R);
      LThumbWidth := RectHeight(R);

      case TickMarks of
        tmBottomRight: begin
          if State = ngsPressed then
            FillHalftonePolygon(Canvas, [Point(R.Left, R.Top),
              Point(R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top),
              Point(R.Left + LThumbHeight - 1, R.Top + LThumbWidth div 2),
              Point(R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top + LThumbWidth - 1),
              Point(R.Left, R.Top + LThumbWidth - 1)],
              BackColor, clWhite)
          else
            FillPolygon(Canvas, [Point(R.Left, R.Top),
              Point(R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top),
              Point(R.Left + LThumbHeight - 1, R.Top + LThumbWidth div 2),
              Point(R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top + LThumbWidth - 1),
              Point(R.Left, R.Top + LThumbWidth - 1)],
              BackColor);

          MoveTo(Canvas, R.Left, R.Top + LThumbWidth - 2);
          LineTo(Canvas, R.Left, R.Top, RaisedColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top, RaisedColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbHeight - 1, R.Top + LThumbWidth div 2, RaisedColor(BackColor, OuterValue));

          MoveTo(Canvas, R.Left + LThumbHeight - 2, R.Top + LThumbWidth div 2);
          LineTo(Canvas, R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top + LThumbWidth - 2, SunkenColor(BackColor, InnerValue));
          LineTo(Canvas, R.Left, R.Top + LThumbWidth - 2, SunkenColor(BackColor, InnerValue));

          MoveTo(Canvas, R.Left + LThumbHeight - 1, R.Top + LThumbWidth div 2);
          LineTo(Canvas, R.Left + LThumbHeight - LThumbWidth div 2 - 1, R.Top + LThumbWidth - 1, SunkenColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left - 1, R.Top + LThumbWidth - 1, SunkenColor(BackColor, OuterValue));
        end;
        tmTopLeft: begin
          if State = ngsPressed then
            FillHalftonePolygon(Canvas, [Point(R.Left + LThumbWidth div 2, R.Top),
              Point(R.Left + LThumbHeight - 1, R.Top),
              Point(R.Left + LThumbHeight - 1, R.Top + LThumbWidth - 1),
              Point(R.Left + LThumbWidth div 2, R.Top + LThumbWidth - 1),
              Point(R.Left, R.Top + LThumbWidth div 2)],
              BackColor, clWhite)
          else
            FillPolygon(Canvas, [Point(R.Left + LThumbWidth div 2, R.Top),
              Point(R.Left + LThumbHeight - 1, R.Top),
              Point(R.Left + LThumbHeight - 1, R.Top + LThumbWidth - 1),
              Point(R.Left + LThumbWidth div 2, R.Top + LThumbWidth - 1),
              Point(R.Left, R.Top + LThumbWidth div 2)],
              BackColor);

          MoveTo(Canvas, R.Left + 1, R.Top + LThumbWidth div 2 - 1);
          LineTo(Canvas, R.Left + LThumbWidth div 2, R.Top, RaisedColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbHeight - 1, R.Top, RaisedColor(BackColor, OuterValue));

          MoveTo(Canvas, R.Left + LThumbHeight - 2, R.Top + 1);
          LineTo(Canvas, R.Left + LThumbHeight - 2, R.Top + LThumbWidth - 2, SunkenColor(BackColor, InnerValue));
          LineTo(Canvas, R.Left + LThumbWidth div 2, R.Top + LThumbWidth - 2, SunkenColor(BackColor, InnerValue));
          LineTo(Canvas, R.Left, R.Top + LThumbWidth div 2 - 1, SunkenColor(BackColor, InnerValue));

          MoveTo(Canvas, R.Left + LThumbHeight - 1, R.Top);
          LineTo(Canvas, R.Left + LThumbHeight - 1, R.Top + LThumbWidth - 1, SunkenColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left + LThumbWidth div 2, R.Top + LThumbWidth - 1, SunkenColor(BackColor, OuterValue));
          LineTo(Canvas, R.Left - 1, R.Top + LThumbWidth div 2 - 1, SunkenColor(BackColor, OuterValue));
        end;
        tmBoth: begin
          if State = ngsPressed then
            FillHalftoneRect(Canvas, Rect(R.Left, R.Top, R.Left + LThumbHeight, R.Top + LThumbWidth), BackColor, clWhite)
          else
            FillRect(Canvas, Rect(R.Left, R.Top, R.Left + LThumbHeight, R.Top + LThumbWidth), BackColor);
          DrawEdge(Canvas, Rect(R.Left, R.Top, R.Left + LThumbHeight, R.Top + LThumbWidth),
            RaisedColor(BackColor, OuterValue), SunkenColor(BackColor, OuterValue));
          DrawEdge(Canvas, Rect(R.Left + 1, R.Top + 1, R.Left + LThumbHeight - 1, R.Top + LThumbWidth - 1),
            BackColor, SunkenColor(BackColor, InnerValue));
        end;
      end;
    end;
end;

procedure TTeThemeKDE.DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect);
var
  Color: TColor;
begin
  Color := GetColor(ngcBtnFace);
  DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, SunkenColor(Color, InnerValue), Color);
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcWindow));

  if not IsRectEmpty(HighlightR) then
  begin
    InflateRect(HighlightR, -3, -3);
    FillRect(Canvas, HighlightR, GetColor(ngcHighlight));
  end;
end;

procedure TTeThemeKDE.DrawProgessFrame(Canvas: TCanvas; R: TRect);
var
  Color: TColor;
begin
  Color := GetColor(ngcBtnFace);
  DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, SunkenColor(Color, InnerValue), Color);
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcWindow));
end;

procedure TTeThemeKDE.DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
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
      InflateRect(BarR, -1, -1);
      FillRect(Canvas, BarR, FillColor);
    end
    else
    begin
      { Segments }
      InflateRect(R, -1, -1);
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
      InflateRect(BarR, -1, -1);
      FillRect(Canvas, BarR, FillColor);
    end
    else
    begin
      { Segments }
      InflateRect(R, -1, -1);
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

procedure TTeThemeKDE.DrawPanel(Canvas: TCanvas; R: TRect; ShowBevel, ShowCaption: boolean);
begin
  { Draw Panel Face }
  if ShowBevel then
  begin
    DrawRect(Canvas, R, GetColor(ngcBorder));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), InnerValue), SunkenColor(GetColor(ngcBtnFace), InnerValue));
    InflateRect(R, -1, -1);
  end;

  FillRect(Canvas,  R, GetColor(ngcWindow));
end;

procedure TTeThemeKDE.DrawPanelCaption(Canvas: TCanvas; R: TRect; Caption: WideString);
begin
  { Draw line }
  MoveTo(Canvas, R.Left, R.Bottom);
  LineTo(Canvas, R.Right, R.Bottom, GetColor(ngcBorder));

  { Draw Caption  }
  FillRect(Canvas, R, GetColor(ngcBtnFace));

  { Draw Text }
  InflateRect(R, -5, 0);
  Canvas.Font.Color := GetColor(ngcWindowText);
  Canvas.Font.Name := GetFontName(ngfWindowText);
  Canvas.Font.Size := GetFontSize(ngfWindowText);
  Canvas.Font.Style:= GetFontStyle(ngfWindowText);
  DrawText(Canvas, Caption, R, DrawTextBiDiModeFlags(DT_LEFT or DT_SINGLELINE or DT_VCenter));
end;

procedure TTeThemeKDE.DrawPanelButton(Canvas: TCanvas; R: TRect;
  Kind: TTePanelButtonKind; State: TTeThemeButtonState; Rolled: boolean);
var
  BackColor: TColor;
  KindFlags, Flags: integer;
  B: TTeBitmap;
  P: TKColorRec;
  i, j: integer;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      BackColor := GetColor(ngcBtnFace);

  InflateRect(R, -1, -1);
  if State = ngsPressed then
  begin
    DrawEdge(Canvas, R, SunkenColor(BackColor, OuterValue), RaisedColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    InflateRect(R, -1, -1);
  end
  else
  begin
    DrawEdge(Canvas, R, SunkenColor(BackColor, OuterValue), RaisedColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, RaisedColor(BackColor, OuterValue), SunkenColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
  end;
  FillRect(Canvas, R, BackColor);

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

procedure TTeThemeKDE.DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect;
  Caption: WideString);
begin
  { Draw Frame }
  MoveTo(Canvas, R.Left, R.Top);
  LineTo(Canvas, CaptionRect.Left, R.Top, RaisedColor(GetColor(ngcBtnFace), InnerValue));
  MoveTo(Canvas, CaptionRect.Right, R.Top);
  LineTo(Canvas, R.Right, R.Top, RaisedColor(GetColor(ngcBtnFace), InnerValue));
  LineTo(Canvas, R.Right, R.Bottom, RaisedColor(GetColor(ngcBtnFace), InnerValue));
  LineTo(Canvas, R.Left, R.Bottom, RaisedColor(GetColor(ngcBtnFace), InnerValue));
  LineTo(Canvas, R.Left, R.Top, RaisedColor(GetColor(ngcBtnFace), InnerValue));

  OffsetRect(R, -1, -1);

  MoveTo(Canvas, R.Left, R.Top);
  LineTo(Canvas, CaptionRect.Left, R.Top, SunkenColor(GetColor(ngcBtnFace), InnerValue));
  MoveTo(Canvas, CaptionRect.Right, R.Top);
  LineTo(Canvas, R.Right, R.Top, SunkenColor(GetColor(ngcBtnFace), InnerValue));
  LineTo(Canvas, R.Right, R.Bottom, SunkenColor(GetColor(ngcBtnFace), InnerValue));
  LineTo(Canvas, R.Left, R.Bottom, SunkenColor(GetColor(ngcBtnFace), InnerValue));
  LineTo(Canvas, R.Left, R.Top, SunkenColor(GetColor(ngcBtnFace), InnerValue));

  { Draw Caption }
  DrawText(Canvas, Caption, CaptionRect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;


procedure TTeThemeKDE.DrawScrollBar(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind);
begin
  FillHalftoneRect(Canvas, R, GetColor(ngcBtnFace), clWhite);
end;

procedure TTeThemeKDE.DrawScrollBarButton(Canvas: TCanvas; R: TRect;
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
    if State = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      BackColor := GetColor(ngcBtnFace);

  if State = ngsPressed then
  begin
    DrawEdge(Canvas, R, SunkenColor(BackColor, OuterValue), RaisedColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, SunkenColor(BackColor, InnerValue), RaisedColor(BackColor, InnerValue));
    InflateRect(R, -1, -1);
  end
  else
  begin
    DrawEdge(Canvas, R, RaisedColor(BackColor, OuterValue), SunkenColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, RaisedColor(BackColor, InnerValue), SunkenColor(BackColor, InnerValue));
    InflateRect(R, -1, -1);
  end;
  FillRect(Canvas, R, BackColor);

  InflateRect(R, 2, 2);
  DrawFrameControlGlyph(Canvas, R, DFC_SCROLL, Flags, clBlack);
end;

procedure TTeThemeKDE.DrawScrollBarSlider(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; State: TTeThemeButtonState);
var
  BackColor: TKColor;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      BackColor := GetColor(ngcBtnFace);

  if State = ngsPressed then
  begin
    DrawEdge(Canvas, R, SunkenColor(BackColor, OuterValue), RaisedColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, SunkenColor(BackColor, InnerValue), RaisedColor(BackColor, InnerValue));
    InflateRect(R, -1, -1);
  end
  else
  begin
    DrawEdge(Canvas, R, RaisedColor(BackColor, OuterValue), SunkenColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, RaisedColor(BackColor, InnerValue), SunkenColor(BackColor, InnerValue));
    InflateRect(R, -1, -1);
  end;
  FillRect(Canvas, R, BackColor);
end;

procedure TTeThemeKDE.DrawTabBorder(Canvas: TCanvas; R: TRect);
begin
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), OuterValue), SunkenColor(GetColor(ngcBtnFace), OuterValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, GetColor(ngcBtnFace), SunkenColor(GetColor(ngcBtnFace), InnerValue));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeKDE.DrawTab(Canvas: TCanvas; R: TRect; TabPosition: TTabPosition; State: TTeThemeButtonState);
var
  Color: TKColor;
begin
  if State = ngsHot then
    Color := GetColor(ngcHotHighlight)
  else
    Color := GetColor(ngcBtnFace);

  case TabPosition of
    tpTop: InflateRect(R, 0, 1);
    tpBottom: InflateRect(R, 0, 1);
    tpLeft: InflateRect(R, 1, 0);
    tpRight: InflateRect(R, 1, 0);
  end;
  DrawEdge(Canvas, R, RaisedColor(Color, OuterValue), SunkenColor(Color, OuterValue));
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

procedure TTeThemeKDE.DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
  TabPosition: TTabPosition; State: TTeThemeButtonState);
begin
  DrawScrollBarButton(Canvas, R, sbHorizontal, LeftTop, State);
end;

procedure TTeThemeKDE.DrawControlFrame(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState);
var
  Color: TColor;
begin
  if State <> ngsDisabled then
    Color := GetColor(ngcWindow)
  else
    Color := GetColor(ngcDisabled);

  FillRect(Canvas, Rect(R.Right - 20, R.Bottom - 20, R.Right, R.Bottom), Color);

  DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), InnerValue), RaisedColor(GetColor(ngcBtnFace), InnerValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), OuterValue), GetColor(ngcBtnFace));
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

procedure TTeThemeKDE.DrawComboButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState);
var
  BackColor: TColor;
  GlyphR: TRect;
begin
  if State = ngsDisabled then
    BackColor := GetColor(ngcDisabled)
  else
    if State = ngsHot then
      BackColor := GetColor(ngcHotHighlight)
    else
      BackColor := GetColor(ngcBtnFace);

  if State = ngsPressed then
  begin
    DrawEdge(Canvas, R, SunkenColor(BackColor, OuterValue), RaisedColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, SunkenColor(BackColor, InnerValue), RaisedColor(BackColor, InnerValue));
    InflateRect(R, -1, -1);
  end
  else
  begin
    DrawEdge(Canvas, R, RaisedColor(BackColor, OuterValue), SunkenColor(BackColor, OuterValue));
    DrawEdge(Canvas, R, RaisedColor(BackColor, OuterValue), SunkenColor(BackColor, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, RaisedColor(BackColor, InnerValue), SunkenColor(BackColor, InnerValue));
    InflateRect(R, -1, -1);
  end;

  FillRect(Canvas, R, BackColor);

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

procedure TTeThemeKDE.DrawSpeedButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
var
  Color: TColor;
begin
  if Flat and (State = ngsNormal) and not Exclusive then Exit;
  if State = ngsDisabled then Exit;

  if State = ngsHot then
    Color := GetColor(ngcHotHighlight)
  else
    Color := GetColor(ngcBtnFace);

  if not Flat then
  begin
    { Standard }
    case State of
      ngsNormal:
        if not Exclusive then
        begin
          DrawEdge(Canvas, R, RaisedColor(Color, OuterValue), SunkenColor(Color, OuterValue));
          InflateRect(R, -1, -1);
          DrawEdge(Canvas, R, RaisedColor(Color, InnerValue), SunkenColor(Color, InnerValue));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, Color);
        end
        else
        begin
          DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
          InflateRect(R, -1, -1);
          DrawEdge(Canvas, R, SunkenColor(Color, InnerValue), RaisedColor(Color, InnerValue));
          InflateRect(R, -1, -1);
          FillHalftoneRect(Canvas, R, Color, clWhite);
        end;
      ngsPressed:
        if not Exclusive then
        begin
          DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
          InflateRect(R, -1, -1);
          DrawEdge(Canvas, R, SunkenColor(Color, InnerValue), RaisedColor(Color, InnerValue));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, Color);
        end
        else
        begin
          DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
          InflateRect(R, -1, -1);
          DrawEdge(Canvas, R, SunkenColor(Color, InnerValue), RaisedColor(Color, InnerValue));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, Color);
        end;
    end;
  end
  else
  begin
    { Flat }
    if State = ngsPressed then
    begin
      DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
      InflateRect(R, -1, -1);
      FillRect(Canvas, R, Color);
    end
    else
      if State = ngsHot then
        if not Exclusive then
        begin
          DrawEdge(Canvas, R, RaisedColor(Color, OuterValue), SunkenColor(Color, OuterValue));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, Color);
        end
        else
        begin
          DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
          InflateRect(R, -1, -1);
          FillHalftoneRect(Canvas, R, Color, clWhite);
        end;

    if Exclusive and (State = ngsNormal) then
    begin
      DrawEdge(Canvas, R, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
      InflateRect(R, -1, -1);
      FillHalftoneRect(Canvas, R, Color, clWhite);
    end;
  end;
end;

procedure TTeThemeKDE.DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect;
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

procedure TTeThemeKDE.DrawSpinButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Up: boolean);
begin
  if State = ngsNormal then
    DrawSpeedButton(Canvas, R, State, false, false)
  else
    DrawSpeedButton(Canvas, R, State, true, false);
  { Draw Glyph }
end;

procedure TTeThemeKDE.DrawControlBar(Canvas: TCanvas; R: TRect);
begin
  { ControlBar Background }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeKDE.DrawControlBarFrame(Canvas: TCanvas; R,
  GrabberRect: TRect);
var
  Color: TKColor;
begin
  { ControlBar Frame }
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcMenuBar), InnerValue),
    SunkenColor(GetColor(ngcMenuBar), InnerValue));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcMenuBar));

  { Draw Grabber }
  InflateRect(GrabberRect, -4, -1);
  DrawEdge(Canvas, GrabberRect, RaisedColor(GetColor(ngcMenuBar), InnerValue),
    SunkenColor(GetColor(ngcMenuBar), InnerValue));
end;

procedure TTeThemeKDE.DrawToolbar(Canvas: TCanvas; R: TRect);
begin
  { Draw toolbar }
  FillRect(Canvas, R, GetColor(ngcMenuBar));
end;

procedure TTeThemeKDE.DrawGridCell(Canvas: TCanvas; R: TRect;
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
    Inc(R.Right);
    Inc(R.Bottom);
    DrawEdge(Canvas, R, RaisedColor(Color, OuterValue), SunkenColor(Color, OuterValue));
    InflateRect(R, -1, -1);
    DrawEdge(Canvas, R, RaisedColor(Color, InnerValue), SunkenColor(Color, InnerValue));
  end;
end;

procedure TTeThemeKDE.DrawSplitter(Canvas: TCanvas; ARect: TRect; AHot, ABeveled: boolean);
begin
  if ABeveled then
  begin
    DrawEdge(Canvas, ARect, RaisedColor(GetColor(ngcMenuBar), InnerValue),
      SunkenColor(GetColor(ngcBtnFace), InnerValue));
    InflateRect(ARect, -1, -1);
  end;

  if not AHot then
  begin
    FillRect(Canvas, ARect, GetColor(ngcBtnFace))
  end
  else
  begin
    FillRect(Canvas, ARect, GetColor(ngcHotHighlight))
  end;
end;

procedure TTeThemeKDE.DrawHeaderSection(Canvas: TCanvas; ARect: TRect;
  Section: TTeHeaderSection; AState: TTeSectionState);
var
  Color, RColor, SColor: TColor;
begin
  { Section may be nil}
   
  if AState = ssDraggedOut then
    Color := clBlack
  else
    if AState in [ssHot, ssDraggin] then
      Color := GetColor(ngcHotHighlight)
    else
      Color := GetColor(ngcBtnFace);

  FillRect(Canvas, ARect, Color);

  { Draw bevel }
  RColor := RaisedColor(Color, InnerValue);
  SColor := SunkenColor(Color, InnerValue);

  if AState in [ssPressed, ssUnderDrag] then
    DrawEdge(Canvas, ARect, SColor, RColor)
  else
    DrawEdge(Canvas, ARect, RColor, SColor)
end;

procedure TTeThemeKDE.DrawStatusBar(Canvas: TCanvas; ARect: TRect);
begin
  FillRect(Canvas, ARect, GetColor(ngcBtnFace));
end;

procedure TTeThemeKDE.DrawStatusGripper(Canvas: TCanvas; ARect: TRect);
begin
  DrawFrameControl(Canvas.Handle, ARect, DFC_SCROLL, DFCS_SCROLLSIZEGRIP);
end;

procedure TTeThemeKDE.DrawStatusPanel(Canvas: TCanvas; ARect: TRect;
  Panel: TTeStatusPanel);
begin
  DrawEdge(Canvas, ARect, SunkenColor(GetColor(ngcBtnFace), InnerValue), RaisedColor(GetColor(ngcBtnFace), InnerValue));
  InflateRect(ARect, -1, -1);
  FillRect(Canvas, ARect, GetColor(ngcBtnFace));
end;

procedure TTeThemeKDE.DrawScrollBox(Canvas: TCanvas; R: TRect;
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

  DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), InnerValue), RaisedColor(GetColor(ngcBtnFace), InnerValue));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), OuterValue), GetColor(ngcBtnFace));
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

function TTeThemeKDE.GetRegion(Width, Height: integer;
  BorderStyle: TTeBorderStyle): HRgn;
begin
  Result := 0;
end;

initialization
  RegisterTheme(TTeThemeKDE);
end.




