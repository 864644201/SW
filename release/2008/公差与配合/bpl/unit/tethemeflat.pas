{==============================================================================

  Flat Theme
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: tethemeflat.pas,v 1.5 2002/10/28 21:04:02 Evgeny Exp $

===============================================================================}

unit tethemeflat;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus,
  Grids, te_controls, KsThemeThemes;

const

  OuterValue = 150;
  InnerValue = 90;
  
type

{ TTeThemeFlat class }

  TTeThemeFlat = class(TTeTheme)
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

    function HasCaptionTextShadow: boolean; override;  

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

uses ksthemeengine;

procedure DrawFrameControlGlyph(Canvas: TCanvas; ARect: TRect; AType, AStyle: cardinal; Color: TColor);
var
  B: TTeBitmap;
  Pixel: PKColor;
  CColor: TKColor;
  i, j: integer;
begin
  { Draw only glyph }
  B := TTeBitmap.Create;
  B.SetSize(RectWidth(ARect), RectHeight(ARect));

  CColor := KColor(Color);

  DrawFrameControl(B.DC, Rect(0, 0, B.Width, B.Height), AType, AStyle);

  for i := 0 to B.Width - 1 do
    for j := 0 to B.Height - 1 do
    begin
      Pixel := @B.Bits[i + j * B.Width];

      if Pixel^ <> 0 then
        Pixel^ := ckTransparent
      else
        Pixel^ := CColor;
    end; 

  B.Draw(Canvas, ARect.Left, ARect.Top, true);

  B.Free;
end;

{ TTeThemeFlat }

constructor TTeThemeFlat.Create;
begin
  inherited Create;
end;

destructor TTeThemeFlat.Destroy;
begin
  inherited Destroy;
end;

{ Metrix ======================================================================}

function TTeThemeFlat.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaption: Result := clActiveCaption;
    ngcInactiveCaption: Result := clInactiveCaption;
    ngcCaptionShadow: Result := clBlack;
    ngcCaptionText: Result := clWhite;
    ngcInactiveCaptionText: Result := clWhite;
    ngcBorder: Result := clBtnShadow;
    ngcWindow: Result := clWindow;
    ngcBtnFace: Result := clBtnFace;
    ngcHighlight: Result := KColorToColor(SunkenColor(KColor(clBtnFace), 30));
    ngcHotHighlight: Result := RGB(182, 189, 210);
    { Menus }
    ngcMenuBorder: Result := clBtnShadow;
    ngcMenuBar: Result := KColorToColor(RaisedColor(KColor(clBtnFace), 10));
    ngcMenuBarHighlight: Result := RGB(182, 189, 210);
    ngcMenuItem: Result := KColorToColor(RaisedColor(KColor(clBtnFace), 10));
    ngcMenuItemHighlight: Result := RGB(182, 189, 210);
    ngcMenuBarText: Result := clMenuText;
    ngcMenuBarHighlightText: Result := clMenuText;
    ngcMenuItemText: Result := clMenuText;
    ngcMenuItemHighlightText: Result := clMenuText;
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

function TTeThemeFlat.GetFontName(Font: TTeThemeFont): string;
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

function TTeThemeFlat.GetFontSize(Font: TTeThemeFont): integer;
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

function TTeThemeFlat.GetFontStyle(Font: TTeThemeFont): TFontStyles;
begin
  case Font of
    ngfCaptionText: Result := [];
    ngfSmCaptionText: Result := [];
    { Menus }
    ngfMenuBarText: Result := [];
    ngfMenuItemText: Result := [];
    { Controls }
    ngfWindowText: Result := [];
  else
    Result := [];
  end;
end;

function TTeThemeFlat.GetMetrix(Metrix: TTeThemeMetrix): integer;
begin
  case Metrix of
    ngmBorderWidth: Result := 3;
    ngmSmBorderWidth: Result := 3;
    ngmCaptionHeight: Result := 23;
    ngmSmCaptionHeight: Result := 20;
    ngmCaptionMargin: Result := 10;
    ngmButtonWidth: Result := 18;
    ngmButtonHeight: Result := 15;
    ngmSmButtonWidth: Result := 14;
    ngmSmButtonHeight: Result := 12;
    ngmButtonMarginX: Result := 6;
    ngmButtonMarginY: Result := 6;
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

function TTeThemeFlat.HasCaptionTextShadow: boolean;
begin
  Result := false;
end;

class function TTeThemeFlat.GetThemeName: string;
begin
  Result := 'Flat';
end;

class function TTeThemeFlat.UseTheme: boolean;
begin
  Result := true;
end;

procedure TTeThemeFlat.ChangeThemeColors;
begin
end;

{ Form Draw ===================================================================}

procedure TTeThemeFlat.DrawSysButton(Canvas: TCanvas; R: TRect; Kind: TTeThemeSysButton;
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
  if Hot then
    Color := GetColor(ngcHotHighlight)
  else
    Color := GetColor(ngcBtnFace);

  FillRect(Canvas, BR, Color); 
  if Down then
  begin
    DrawEdge(Canvas, BR, SunkenColor(Color, OuterValue), RaisedColor(Color, OuterValue));
    InflateRect(BR, -1, -1);
    DrawEdge(Canvas, BR, SunkenColor(Color, InnerValue), Color);
  end
  else
  begin
    DrawEdge(Canvas, BR, RaisedColor(Color, OuterValue), SunkenColor(Color, OuterValue));
    InflateRect(BR, -1, -1);
    DrawEdge(Canvas, BR, Color, SunkenColor(Color, InnerValue));
  end;

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

procedure TTeThemeFlat.DrawWindow(Canvas: TCanvas; Width,
  Height: integer; ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle);
var
  BorderRect: TRect;
  CaptionRect: TRect;
  Color, RightColor: TColor;
begin
  CaptionRect := ClientRect;
  CaptionRect.Bottom := CaptionRect.Top;
  CaptionRect.Top := ClientRect.Left;
  { Draw Caption }
  if Active then
  begin
    Color := GetColor(ngcCaption);
    RightColor := GetSysColor(COLOR_GRADIENTACTIVECAPTION);
  end
  else
  begin
    Color := GetColor(ngcInactiveCaption);
    RightColor := GetSysColor(COLOR_GRADIENTINACTIVECAPTION);
  end;

  FillGradientRect(Canvas, CaptionRect, Color, RightColor, false);

  { Draw Border }
  BorderRect := Rect(0, 0, Width, Height);
  DrawEdge(Canvas, BorderRect, RaisedColor(GetColor(ngcBtnFace), OuterValue), SunkenColor(GetColor(ngcBtnFace), OuterValue));
  InflateRect(BorderRect, -1, -1);
  DrawEdge(Canvas, BorderRect, GetColor(ngcBtnFace), SunkenColor(GetColor(ngcBtnFace), InnerValue));
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, GetColor(ngcBtnFace)); 
end;

{ Menus Draw ==================================================================}

procedure TTeThemeFlat.CalcMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; var AWidth, AHeight: integer);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

  AWidth := Canvas.TextWidth(Item.Caption) + 12;
  AHeight := GetMetrix(ngmMenuBarHeight);
end;

procedure TTeThemeFlat.CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
  var AWidth, AHeight: integer);
begin
  Canvas.Font.Name := GetFontName(ngfMenuItemText);
  Canvas.Font.Size := GetFontSize(ngfMenuItemText);
  Canvas.Font.Style := GetFontStyle(ngfMenuItemText);

  if Item.Caption = '-' then
    AHeight := 9
  else
    AHeight := GetMetrix(ngmMenuBarHeight);
  AWidth := GlyphWidth + Canvas.TextWidth(Item.Caption) + ItemStep + Canvas.TextWidth(ShortCutToText(Item.ShortCut)) + SubMenuWidth;
end;

procedure TTeThemeFlat.DrawMenuBar(Canvas: TCanvas; Width,
  Height: integer);
begin
  FillRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcMenuBar));
  DrawRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcMenuBorder));
end;

procedure TTeThemeFlat.DrawPopupMenu(Canvas: TCanvas; Width,
  Height: integer);
var
  R: TRect;
begin
  R := Rect(0, 0, Width, Height);
  DrawRect(Canvas, R, GetColor(ngcMenuItem));
  DrawRect(Canvas, R, GetColor(ngcMenuBorder));
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, GetColor(ngcMenuItem));
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, GetColor(ngcMenuItem));
end;

procedure TTeThemeFlat.DrawMenuBarItem(Canvas: TCanvas;
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
      DrawRect(Canvas, Rect, GetColor(ngcMenuBorder));
    end;
  end
  else
  begin
    if Item.IsToolbar then
      DrawSpeedButton(Canvas, Rect, ngsNormal, true, false);

    Canvas.Font.Color := GetColor(ngcMenuBarText);
  end;

  { Text }
  DrawText(Canvas, Item.Caption, Rect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;

procedure TTeThemeFlat.DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
var
  Flags: integer;
  Color: TColor;
begin
  { MDI Items }
  InflateRect(Rect, -1, -1);
  
  FillRect(Canvas, Rect, GetColor(ngcMenuItem));

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
         { Draw border }
         DrawRect(Canvas, Rect, GetColor(ngcMenuBorder));
       end
       else
       begin
         Color := GetColor(ngcMenuItem);
         FillRect(Canvas, Rect, Color);
       end;
     end;
     mikMinimize: begin
       InflateRect(Rect, -2, -2);

       Flags := Flags or DFCS_CAPTIONMIN;
       DrawFrameControl(Canvas.Handle, Rect, DFC_CAPTION, Flags);
       DrawRect(Canvas, Rect, GetColor(ngcBorder));
     end;
     mikRestore: begin
       InflateRect(Rect, -2, -2);

       Flags := Flags or DFCS_CAPTIONRESTORE;
       DrawFrameControl(Canvas.Handle, Rect, DFC_CAPTION, Flags);
       DrawRect(Canvas, Rect, GetColor(ngcBorder));
     end;
     mikClose: begin
       InflateRect(Rect, -2, -2);

       Flags := Flags or DFCS_CAPTIONCLOSE;
       DrawFrameControl(Canvas.Handle, Rect, DFC_CAPTION, Flags);
       DrawRect(Canvas, Rect, GetColor(ngcBorder));
     end;
  end;
end;

procedure TTeThemeFlat.DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
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
  Color: TColor;
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
    DrawRect(Canvas, Rect, GetColor(ngcMenuBorder));
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

    Canvas.Brush.Color := Canvas.Font.Color;
    Canvas.RoundRect(Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, 5, 5);
  end;
end;

procedure TTeThemeFlat.DrawMenuScrollButton(Canvas: TCanvas; Rect: TRect;
  Button: TTeMenuScrollButton; Active: boolean);
var
  R: TRect;
begin
  if Active then
  begin
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));
    DrawEdge(Canvas, Rect, SunkenColor(GetColor(ngcMenuBorder), 30), RaisedColor(GetColor(ngcMenuBorder), 30));
  end
  else
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));

  Canvas.Font.Color := GetColor(ngcMenuItemText);

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

procedure TTeThemeFlat.DrawButton(Canvas: TCanvas; Width, Height: integer;
  State: TTeThemeButtonState);
var
  R: TRect;
  BackColor: TColor;
  RColor, SColor: TColor;
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
  SColor := SunkenColor(BackColor, 30);
  { Draw Border }
  if State <> ngsDisabled then
  begin
    { Enabled }
    DrawRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcBorder));
    InflateRect(R, -1, -1);
    { Draw State }
    if State = ngsPressed then
    begin
      DrawEdge(Canvas, R, SColor, RColor);
      InflateRect(R, -1, -1);
      FillRect(Canvas, R, BackColor);
    end
    else
    begin
      if State = ngsHot then
      begin
        MoveTo(Canvas, 1, R.Top);
        LineTo(Canvas, R.Right-1, R.Top, RColor);

        MoveTo(Canvas, R.Left, 1);
        LineTo(Canvas, R.Left, R.Bottom-1, RColor);
        InflateRect(R, -1, -1);
      end;

      if (State in [ngsFocused, ngsDefault]) then
      begin
        MoveTo(Canvas, 1, R.Bottom-1);
        LineTo(Canvas, R.Right-1, R.Bottom-1, SColor);

        MoveTo(Canvas, R.Right-1, 1);
        LineTo(Canvas, R.Right-1, R.Bottom-1, SColor);
      end;

      FillRect(Canvas, R, BackColor);
    end;
  end
  else
  begin
    { Disabled }
//    DrawRoundRect(Canvas, Rect(0, 0, Width, Height), 3, GetColor(ngcDisabled)));
  end;
end;

procedure TTeThemeFlat.DrawCheckBox(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  BackColor, CheckColor: TColor;
  R: TRect;
begin
  R := Rect;
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
        BackColor := GetColor(ngcBtnFace);

    CheckColor := GetColor(ngcBorder);
    DrawRect(Canvas, Rect, GetColor(ngcBorder));
  end
  else
  begin
    { Disables }
    BackColor := GetColor(ngcBtnFace);
    CheckColor := GetColor(ngcDisabled);
    DrawRect(Canvas, Rect, GetColor(ngcBorder));
  end;

  { Draw Border }
  InflateRect(Rect, -1, -1);
  FillRect(Canvas, Rect, BackColor);
  InflateRect(Rect, -1, -1);

  { Draw }
  case CheckState of
    ngcChecked: DrawFrameControlGlyph(Canvas, R, DFC_BUTTON, DFCS_BUTTONCHECK or DFCS_CHECKED, CheckColor);
    ngcMixed: begin
      { Draw Grayed }
      InflateRect(Rect, -1, -1);
      FillRect(Canvas, Rect, CheckColor);
    end;
  end;
end;

procedure TTeThemeFlat.DrawRadioButton(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  BackColor, CheckColor: TColor;
begin
  { Draw radio button }
  with Canvas do
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
          BackColor := GetColor(ngcBtnFace);

      CheckColor := GetColor(ngcBorder);
      Canvas.Pen.Style := psSolid;
      Canvas.Pen.Color := CheckColor;
      Canvas.Brush.Style := bsClear;
      Canvas.RoundRect(Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, 6, 6);
    end
    else
    begin
      { Disables }
      BackColor := GetColor(ngcBtnFace);
      CheckColor := ckBlack;
      Canvas.Pen.Style := psSolid;
      Canvas.Pen.Color := CheckColor;
      Canvas.Brush.Style := bsClear;
      Canvas.RoundRect(Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, 6, 6);
    end;

    { Draw Border }
    InflateRect(Rect, -1, -1);

    Canvas.Pen.Style := psSolid;
    Canvas.Brush.Style := bsSolid;
    Canvas.Pen.Color := BackColor;
    Canvas.Brush.Color := BackColor;
    Canvas.RoundRect(Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, 2, 2);

    { Draw }
    if CheckState = ngcChecked then
    begin
      { Draw Check }
      InflateRect(Rect, -2, -2);
      Canvas.Pen.Color := CheckColor;
      Canvas.Brush.Color := CheckColor;
      Canvas.RoundRect(Rect.Left, Rect.Top, Rect.Right, Rect.Bottom, 2, 2);
    end;
  end
end;

procedure TTeThemeFlat.DrawTrackBarThumb(Canvas: TCanvas; R: TRect;
  Orientation: TTrackOrientation; TickMarks: TTickMark;
  State: TTeThemeButtonState);
var
  BackColor: TColor;
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

          DrawPolygon(Canvas, [Point(R.Left, R.Bottom),
            Point(R.Right - 1, R.Bottom),
            Point(R.Right - 1, R.Top + RectWidth(R) div 2),
            Point(R.Left + RectWidth(R) div 2, R.Top),
            Point(R.Left, R.Top + RectWidth(R) div 2)],
            GetColor(ngcBorder));
        end;
        tmBoth: begin
          DrawRect(Canvas, R, GetColor(ngcBorder));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, BackColor);
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

          DrawPolygon(Canvas, [Point(R.Left + RectHeight(R) div 2, R.Top),
            Point(R.Right, R.Top),
            Point(R.Right, R.Bottom - 1),
            Point(R.Left + RectHeight(R) div 2, R.Bottom - 1),
            Point(R.Left, R.Bottom - RectHeight(R) div 2 - 1)],
            GetColor(ngcBorder));
        end;
        tmBoth: begin
          DrawRect(Canvas, R, GetColor(ngcBorder));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, BackColor);
        end;
      end;
    end;
end;

procedure TTeThemeFlat.DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect);
begin
  FillRect(Canvas, R, GetColor(ngcBtnFace));
  DrawRect(Canvas, R, GetColor(ngcBorder));

  if not IsRectEmpty(HighlightR) then
  begin
    InflateRect(HighlightR, -1, -1);
    FillRect(Canvas, HighlightR, GetColor(ngcHighlight));
  end;
end;

procedure TTeThemeFlat.DrawProgessFrame(Canvas: TCanvas; R: TRect);
var
  BackColor: TColor;
  BorderColor: TColor;
begin
  BackColor := GetColor(ngcWindow);
  BorderColor := GetColor(ngcBorder);

  DrawRect(Canvas, R, BorderColor);
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, BackColor);
end;

procedure TTeThemeFlat.DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
  Orientation: TTeBarOrientation; Smooth: boolean);
const
  SegRation   = 0.64; { Segment width to height ration }
  SegSpacing  = 2;    { Spacing b/w segment }
var
  i: integer;
  SegW, SegH, FillSegCount: integer;
  FillColor: TColor;
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

procedure TTeThemeFlat.DrawPanel(Canvas: TCanvas; R: TRect; ShowBevel, ShowCaption: boolean);
begin
  { Draw Panel Face }
  if ShowBevel then
  begin
    DrawRect(Canvas, R, GetColor(ngcBorder));
    InflateRect(R, -1, -1);
  end;
  FillRect(Canvas, R, GetColor(ngcWindow));
end;

procedure TTeThemeFlat.DrawPanelCaption(Canvas: TCanvas; R: TRect; Caption: WideString);
begin
  { Draw Caption  }
  FillRect(Canvas, R, GetColor(ngcHotHighlight));

  { Draw line }
  MoveTo(Canvas, R.Left, R.Bottom);
  LineTo(Canvas, R.Right, R.Bottom, GetColor(ngcBorder));

  { Draw Text }
  InflateRect(R, -5, 0);
  Canvas.Font.Color := GetColor(ngcWindowText);
  Canvas.Font.Name := GetFontName(ngfWindowText);
  Canvas.Font.Size := GetFontSize(ngfWindowText);
  Canvas.Font.Style:= GetFontStyle(ngfWindowText);
  DrawText(Canvas, Caption, R, DrawTextBiDiModeFlags(DT_LEFT or DT_SINGLELINE or DT_VCenter));
end;

procedure TTeThemeFlat.DrawPanelButton(Canvas: TCanvas; R: TRect;
  Kind: TTePanelButtonKind; State: TTeThemeButtonState; Rolled: boolean);
var
  BackColor: TColor;
  KindFlags, Flags: integer;
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

  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, BackColor);
  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    if State = ngsHot then
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

  if State = ngsPressed then
    DrawFrameControlGlyph(Canvas, R, KindFlags, Flags or DFCS_PUSHED, clBlack)
  else
    DrawFrameControlglyph(Canvas, R, KindFlags, Flags, clBlack);
end;

procedure TTeThemeFlat.DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect; Caption: WideString);
begin
  { Draw Frame }
  MoveTo(Canvas, R.Left, R.Top);
  LineTo(Canvas, CaptionRect.Left, R.Top, GetColor(ngcBorder));
  MoveTo(Canvas, CaptionRect.Right, R.Top);
  LineTo(Canvas, R.Right, R.Top, GetColor(ngcBorder));
  LineTo(Canvas, R.Right, R.Bottom, GetColor(ngcBorder));
  LineTo(Canvas, R.Left, R.Bottom, GetColor(ngcBorder));
  LineTo(Canvas, R.Left, R.Top, GetColor(ngcBorder));

  { Draw Caption }
  DrawText(Canvas, Caption, CaptionRect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;


procedure TTeThemeFlat.DrawScrollBar(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind);
begin
  DrawRect(Canvas, R, GetColor(ngcBorder));
end;

procedure TTeThemeFlat.DrawScrollBarButton(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; LeftTop: boolean; State: TTeThemeButtonState);
var
  Flags: UINT;
  BackColor: TColor;
  SaveR: TRect;
begin
  SaveR := R;
  
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
  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    if State = ngsHot then
      DrawEdge(Canvas, R, RaisedColor(BackColor, 30), SunkenColor(BackColor, 30));

  DrawFrameControlGlyph(Canvas, SaveR, DFC_SCROLL, Flags, clBlack);
end;

procedure TTeThemeFlat.DrawScrollBarSlider(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; State: TTeThemeButtonState);
var
  BackColor: TColor;
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

  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  FillRect(Canvas, R, BackColor);

  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    if State = ngsHot then
      DrawEdge(Canvas, R, RaisedColor(BackColor, 30), SunkenColor(BackColor, 30))
end;

procedure TTeThemeFlat.DrawTabBorder(Canvas: TCanvas; R: TRect);
begin
  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeFlat.DrawTab(Canvas: TCanvas; R: TRect; TabPosition: TTabPosition; State: TTeThemeButtonState);
var
  Color: TColor;
begin
  if State = ngsFocused then
    Color := GetColor(ngcBtnFace)
  else
    if (State = ngsHot) then
      Color := GetColor(ngcHotHighlight)
    else
      Color := GetColor(ngcHighlight);

  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);

  case TabPosition of
    tpTop: R.Bottom := R.Bottom + 1;
    tpBottom: R.Top := R.Top - 1;
    tpLeft: R.Right := R.Right + 1;
    tpRight: R.Left := R.Left - 1;
  end;
  FillRect(Canvas, R, Color);
end;

procedure TTeThemeFlat.DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
  TabPosition: TTabPosition; State: TTeThemeButtonState);
begin
  DrawScrollBarButton(Canvas, R, sbHorizontal, LeftTop, State);
end;

procedure TTeThemeFlat.DrawControlFrame(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState);
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

procedure TTeThemeFlat.DrawComboButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState);
var
  BackColor: TColor;
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

  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, BackColor);

  if State = ngsPressed then
    DrawEdge(Canvas, R, SunkenColor(BackColor, 30), RaisedColor(BackColor, 30))
  else
    if State = ngsHot then
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
      Point((Right - 1 + Left) div 2, Bottom - 1)], clBlack);
  end;
end;

procedure TTeThemeFlat.DrawSpeedButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
begin
  if State = ngsDisabled then Exit;

  if not Flat then
  begin
    { Standard }
    case State of
      ngsNormal:
        if not Exclusive then
        begin
          DrawRect(Canvas, R, GetColor(ngcBorder));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, GetColor(ngcBtnFace));
          DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));
        end
        else
        begin
          FillRect(Canvas, R, GetColor(ngcHighlight));
          DrawRect(Canvas, R, GetColor(ngcBorder));
        end;
      ngsPressed:
        if not Exclusive then
        begin
          DrawRect(Canvas, R, GetColor(ngcBorder));
          InflateRect(R, -1, -1);
          FillRect(Canvas, R, GetColor(ngcBtnFace));
          DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), 30), RaisedColor(GetColor(ngcBtnFace), 30));
        end
        else
        begin
          FillRect(Canvas, R, GetColor(ngcHighlight));
          DrawRect(Canvas, R, GetColor(ngcBorder));
        end;
      ngsDisabled:
        begin
          FillRect(Canvas, R, GetColor(ngcBtnFace));
          DrawRect(Canvas, R, GetColor(ngcDisabled));
        end;
    end;
  end
  else
  begin
    { Flat }
    if State = ngsDisabled then
    begin
      FillRect(Canvas, R, GetColor(ngcWindow));
      DrawRect(Canvas, R, GetColor(ngcDisabled));
    end
    else
    begin
      if State = ngsPressed then
      begin
        DrawRect(Canvas, R, GetColor(ngcBorder));
        InflateRect(R, -1, -1);
        FillRect(Canvas, R, GetColor(ngcBtnFace));
        DrawEdge(Canvas, R, SunkenColor(GetColor(ngcBtnFace), 30), RaisedColor(GetColor(ngcBtnFace), 30));
      end
      else
        if State = ngsHot then
          if not Exclusive then
          begin
            DrawRect(Canvas, R, GetColor(ngcBorder));
            InflateRect(R, -1, -1);
            FillRect(Canvas, R, GetColor(ngcHotHighlight));
            DrawEdge(Canvas, R, RaisedColor(GetColor(ngcHotHighlight), 30), SunkenColor(GetColor(ngcHotHighlight), 30));
          end
          else
          begin
            DrawRect(Canvas, R, GetColor(ngcBorder));
            InflateRect(R, -1, -1);
            FillRect(Canvas, R, GetColor(ngcHotHighlight));
            DrawEdge(Canvas, R, RaisedColor(GetColor(ngcHotHighlight), 30), SunkenColor(GetColor(ngcHotHighlight), 30));
          end
        else
        begin
          FillRect(Canvas, R, GetColor(ngcBtnFace));
          DrawRect(Canvas, R, GetColor(ngcBorder));
        end;
    end;

    if Exclusive and (State = ngsNormal) then
    begin
      FillRect(Canvas, R, GetColor(ngcHighlight));
      DrawRect(Canvas, R, GetColor(ngcBorder));
    end;
  end;
end;

procedure TTeThemeFlat.DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect;
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
        clBlack);
    end
    else
    begin
      MoveTo(Canvas, (Right + Left) div 2, Bottom - 1);
      LineTo(Canvas, Left, Top, GetColor(ngcDisabled));
      LineTo(Canvas, Right, Top, GetColor(ngcDisabled));
      MoveTo(Canvas, Right, Top);
      LineTo(Canvas, (Right + Left) div 2 + 1 - 1, Bottom + 1 - 1, GetColor(ngcDisabled));
    end;
end;

procedure TTeThemeFlat.DrawSpinButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Up: boolean);
begin
  if State = ngsNormal then
    DrawSpeedButton(Canvas, R, State, true, false)
  else
    DrawSpeedButton(Canvas, R, State, true, false);
  { Draw Glyph }
end;

procedure TTeThemeFlat.DrawControlBar(Canvas: TCanvas; R: TRect);
begin
  { ControlBar Background }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeFlat.DrawControlBarFrame(Canvas: TCanvas; R,
  GrabberRect: TRect);
var
  Color: TColor;
begin
  { ControlBar Frame }
  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, GetColor(ngcBtnFace));

  { Draw Grabber }
  Color := GetColor(ngcHotHighlight);

  InflateRect(GrabberRect, -3, -1);
  DrawRect(Canvas, GrabberRect, GetColor(ngcBorder));

  InflateRect(GrabberRect, -1, -1);
  FillRect(Canvas, GrabberRect, Color);
end;

procedure TTeThemeFlat.DrawToolbar(Canvas: TCanvas; R: TRect);
begin
  { Draw toolbar }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeFlat.DrawGridCell(Canvas: TCanvas; R: TRect;
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
end;

procedure TTeThemeFlat.DrawSplitter(Canvas: TCanvas; ARect: TRect; AHot, ABeveled: boolean);
begin
  if not AHot then
    FillRect(Canvas, ARect, GetColor(ngcWindow))
  else
    FillRect(Canvas, ARect, GetColor(ngcHighlight));

  if ABeveled then
    DrawRect(Canvas, ARect, GetColor(ngcBorder));
end;

function TTeThemeFlat.GetRegion(Width, Height: integer;
  BorderStyle: TTeBorderStyle): HRgn;
begin
  Result := 0;
end;

procedure TTeThemeFlat.DrawHeaderSection(Canvas: TCanvas; ARect: TRect;
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

  FillRect(Canvas, ARect, Color);
  DrawRect(Canvas, ARect, GetColor(ngcMenuBorder));
  DrawRect(Canvas, ARect, GetColor(ngcBorder));

  { Draw bevel }
  InflateRect(ARect, -1, -1);
  RColor := RaisedColor(Color, 30);
  SColor := SunkenColor(Color, 30);

  if AState in [ssPressed, ssUnderDrag] then
    DrawEdge(Canvas, ARect, SColor, RColor)
  else
    if AState <> ssNormal then
      DrawEdge(Canvas, ARect, RColor, SColor)
end;

procedure TTeThemeFlat.DrawStatusBar(Canvas: TCanvas; ARect: TRect);
begin
  FillRect(Canvas, ARect, GetColor(ngcBtnFace));
end;

procedure TTeThemeFlat.DrawStatusGripper(Canvas: TCanvas; ARect: TRect);
begin
  with ARect do
  begin
    InflateRect(ARect, -4, -4);
    OffsetRect(ARect, 3, 3);

    FillPolygon(Canvas, [Point(Right, Top - 4), Point(Right, Top), Point(Left, Bottom), Point(Left - 4, Bottom)], GetColor(ngcHighlight));
    DrawPolygon(Canvas, [Point(Right, Top - 4), Point(Right, Top), Point(Left, Bottom), Point(Left - 4, Bottom)], GetColor(ngcBorder));

    FillPolygon(Canvas, [Point(Right, Top + 3), Point(Right, Bottom), Point(Left + 3, Bottom)], GetColor(ngcHighlight));
    DrawPolygon(Canvas, [Point(Right, Top + 3), Point(Right, Bottom), Point(Left + 3, Bottom)], GetColor(ngcBorder));
   end;
end;

procedure TTeThemeFlat.DrawStatusPanel(Canvas: TCanvas; ARect: TRect;
  Panel: TTeStatusPanel);
begin
  FillRect(Canvas, ARect, GetColor(ngcBtnFace));
  InflateRect(ARect, -1, -1);
  DrawRect(Canvas, ARect, GetColor(ngcMenuBorder));
  DrawRect(Canvas, ARect, GetColor(ngcBorder));
end;

procedure TTeThemeFlat.DrawScrollBox(Canvas: TCanvas; R: TRect;
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
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
end;

initialization
  RegisterTheme(TTeThemeFlat);
end.




