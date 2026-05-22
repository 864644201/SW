{==============================================================================

  Luna Theme
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: TeThemeLuna.pas,v 1.4 2002/10/28 21:04:00 Evgeny Exp $

===============================================================================}

unit tethemeluna;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus, Grids,
  te_controls, KsThemeThemes;

type

{ TTeThemeLuna class }

  TTeThemeLuna = class(TTeTheme)
  private
  protected
    ActiveWindow: TTeBitmap;
    InactiveWindow: TTeBitmap;
    procedure ReloadBitmaps; virtual;
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

{$R *.res}

var
  ResStream: TResourceStream;

{ TTeThemeLuna }

constructor TTeThemeLuna.Create;
begin
  inherited Create;
  ReloadBitmaps;
end;

destructor TTeThemeLuna.Destroy;
begin
  InactiveWindow.Free;
  ActiveWindow.Free;
  inherited Destroy;
end;

procedure TTeThemeLuna.ReloadBitmaps;
begin
  if InactiveWindow <> nil then
    InactiveWindow.Free;
  if ActiveWindow <> nil then
    ActiveWindow.Free;
  { }
  ActiveWindow := TTeBitmap.Create;
  ResStream := TResourceStream.Create(HInstance, PChar('XP_MAIN'), RT_RCDATA);
  try
    ActiveWindow.LoadFromPcxStream(ResStream);
    if not ActiveWindow.Empty then
      ActiveWindow.PerformTransparent(ActiveWindow.Pixels[0, 0]);
  finally
    ResStream.Free;
  end;
  { }
  InactiveWindow := TTeBitmap.Create;
  ResStream := TResourceStream.Create(HInstance, PChar('XP_MAIN1'), RT_RCDATA);
  try
    InactiveWindow.LoadFromPcxStream(ResStream);
    if not InactiveWindow.Empty then
      InactiveWindow.PerformTransparent(InactiveWindow.Pixels[0, 0]);
  finally
    ResStream.Free;
  end;
end;

{ Metrix ======================================================================}

function TTeThemeLuna.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaptionText: Result := clWhite;
    ngcInactiveCaptionText: Result := clWhite;
    ngcCaptionShadow: Result := clBlack;
    ngcBorder: Result := RGB(49, 106, 197);
    ngcWindow: Result := RGB(255, 251, 247);
    ngcHighlight: Result := RGB(33, 195, 33);
    ngcBtnFace: Result := RGB(239, 235, 222);
    { Menus }
    ngcMenuBorder: Result := RGB(128, 128, 128);
    ngcMenuBar: Result := RGB(255, 255, 255);
    ngcMenuBarHighlight: Result := RGB(49, 106, 197);
    ngcMenuItem: Result := RGB(255, 255, 255);
    ngcMenuItemHighlight: Result := RGB(49, 106, 197);
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

function TTeThemeLuna.GetFontName(Font: TTeThemeFont): string;
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

function TTeThemeLuna.GetFontSize(Font: TTeThemeFont): integer;
begin
  case Font of
    ngfCaptionText: Result := 10;
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

function TTeThemeLuna.GetFontStyle(Font: TTeThemeFont): TFontStyles;
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

function TTeThemeLuna.GetMetrix(Metrix: TTeThemeMetrix): integer;
begin
  case Metrix of
    ngmBorderWidth: Result := 3;
    ngmSmBorderWidth: Result := 2;
    ngmCaptionHeight: Result := 29;
    ngmSmCaptionHeight: Result := 19;
    ngmCaptionMargin: Result := 10;
    ngmButtonWidth: Result := 21;
    ngmButtonHeight: Result := 21;
    ngmSmButtonWidth: Result := 14;
    ngmSmButtonHeight: Result := 14;
    ngmButtonMarginX: Result := 4;
    ngmButtonMarginY: Result := 4;
    ngmButtonSpace: Result := 3;
    ngmSmButtonMarginX: Result := 3;
    ngmSmButtonMarginY: Result := 3;
    { Menus }
    ngmMenuBarHeight: Result := 22;
    ngmMenuItemHeight: Result := 17;
    { Controls }
    ngmCheckBoxWidth: Result := 16;
    ngmCheckBoxHeight: Result := 16;
    ngmRadioButtonWidth: Result := 16;
    ngmRadioButtonHeight: Result := 16;
    ngmTrackThumbWidth: Result := 11;
    ngmTrackThumbHeight: Result := 22;
    ngmTrackBarHeight: Result := 4;
    ngmPanelCaptionHeight: Result := 22;
    ngmScrollBarHeight: Result := 17;
    ngmSliderWidth: Result := 17;
    ngmTabMargin: Result := 2;
    ngmTabHeight: Result := 21;
    ngmComboButtonWidth: Result := 15;
    ngmGrabberSize: Result := 11;
  else
    Result := 0;
  end;
end;

class function TTeThemeLuna.GetThemeName: string;
begin
  Result := 'Luna';
end;

class function TTeThemeLuna.UseTheme: boolean;
begin
  Result := true;
end;

procedure TTeThemeLuna.ChangeThemeColors;
begin
  ReloadBitmaps;
  ActiveWindow.ChangeBitmapHue(DeltaHue);
  InactiveWindow.ChangeBitmapHue(DeltaHue);
  ActiveWindow.ChangeBitmapBrightness(DeltaBrightness);
  InactiveWindow.ChangeBitmapBrightness(DeltaBrightness);
end;

{ Form Draw ===================================================================}

procedure TTeThemeLuna.DrawSysButton(Canvas: TCanvas; R: TRect; Kind: TTeThemeSysButton;
  Hot, Down, Active: boolean; BorderStyle: TTeBorderStyle);
var
  Window: TTeBitmap;
begin
  if Active then Window := ActiveWindow
  else Window := InactiveWindow;
  
  case Kind of
    ngbClose:
      case BorderStyle of
        kbsStandard: begin
          if Down then
            Window.Draw(Canvas, R.Left, R.Top, Rect(42, 70, 42+GetMetrix(ngmButtonWidth), 70+GetMetrix(ngmButtonHeight)))
          else
            if Hot then
              Window.Draw(Canvas, R.Left, R.Top, Rect(21, 70, 21+GetMetrix(ngmButtonWidth), 70+GetMetrix(ngmButtonHeight)))
            else
              Window.Draw(Canvas, R.Left, R.Top, Rect(0, 70, GetMetrix(ngmButtonWidth), 70+GetMetrix(ngmButtonHeight)));
        end;
        kbsToolWindow: begin
          if Down then
            Window.Draw(Canvas, R.Left, R.Top, Rect(94, 70, 94+GetMetrix(ngmSmButtonWidth), 70+GetMetrix(ngmSmButtonHeight)))
          else
            if Hot then
              Window.Draw(Canvas, R.Left, R.Top, Rect(80, 70, 80+GetMetrix(ngmSmButtonWidth), 70+GetMetrix(ngmSmButtonHeight)))
            else
              Window.Draw(Canvas, R.Left, R.Top, Rect(66, 70, 66+GetMetrix(ngmSmButtonWidth), 70+GetMetrix(ngmSmButtonHeight)));
        end;
      end;
    ngbHelp:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 100, 42+GetMetrix(ngmButtonWidth), 100+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 100, 21+GetMetrix(ngmButtonWidth), 100+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 100, GetMetrix(ngmButtonWidth), 100+GetMetrix(ngmButtonHeight)));
    ngbMin:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 160, 42+GetMetrix(ngmButtonWidth), 160+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 160, 21+GetMetrix(ngmButtonWidth), 160+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 160, GetMetrix(ngmButtonWidth), 160+GetMetrix(ngmButtonHeight)));
    ngbMax:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 130, 42+GetMetrix(ngmButtonWidth), 130+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 130, 21+GetMetrix(ngmButtonWidth), 130+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 130, GetMetrix(ngmButtonWidth), 130+GetMetrix(ngmButtonHeight)));
    ngbRestore:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 190, 42+GetMetrix(ngmButtonWidth), 190+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 190, 21+GetMetrix(ngmButtonWidth), 190+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 190, GetMetrix(ngmButtonWidth), 190+GetMetrix(ngmButtonHeight)));
    ngbRolldown:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 220, 42+GetMetrix(ngmButtonWidth), 220+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 220, 21+GetMetrix(ngmButtonWidth), 220+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 220, GetMetrix(ngmButtonWidth), 220+GetMetrix(ngmButtonHeight)));
    ngbRollup:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 280, 42+GetMetrix(ngmButtonWidth), 280+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 280, 21+GetMetrix(ngmButtonWidth), 280+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 280, GetMetrix(ngmButtonWidth), 280+GetMetrix(ngmButtonHeight)));
    ngbTray:
      if Down then
        Window.Draw(Canvas, R.Left, R.Top, Rect(42, 250, 42+GetMetrix(ngmButtonWidth), 250+GetMetrix(ngmButtonHeight)))
      else
        if Hot then
          Window.Draw(Canvas, R.Left, R.Top, Rect(21, 250, 21+GetMetrix(ngmButtonWidth), 250+GetMetrix(ngmButtonHeight)))
        else
          Window.Draw(Canvas, R.Left, R.Top, Rect(0, 250, GetMetrix(ngmButtonWidth), 250+GetMetrix(ngmButtonHeight)));
  end;
end;

procedure TTeThemeLuna.DrawWindow(Canvas: TCanvas; Width,
  Height: integer; ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle);
var
  Window: TTeBitmap;
begin
  if Active then Window := ActiveWindow
  else Window := InactiveWindow;
  { Draw border }
  case BorderStyle of
    kbsStandard: begin
      { Caption }
      Window.Draw(Canvas, 0, 0, Rect(0, 0, GetMetrix(ngmCaptionMargin), GetMetrix(ngmCaptionHeight)));
      Window.Draw(Canvas, Rect(GetMetrix(ngmCaptionMargin), 0, Width-GetMetrix(ngmCaptionMargin), GetMetrix(ngmCaptionHeight)),
        Rect(GetMetrix(ngmCaptionMargin), 0, 66-GetMetrix(ngmCaptionMargin), GetMetrix(ngmCaptionHeight)));
      Window.Draw(Canvas, Width-GetMetrix(ngmCaptionMargin), 0, Rect(66-GetMetrix(ngmCaptionMargin), 0, 66, GetMetrix(ngmCaptionHeight)));
      { Bottom }
      Window.Draw(Canvas, 0, Height-GetMetrix(ngmBorderWidth), Rect(0, 57, GetMetrix(ngmBorderWidth), 57 + GetMetrix(ngmBorderWidth)));
      Window.Draw(Canvas, Rect(GetMetrix(ngmBorderWidth), Height-GetMetrix(ngmBorderWidth), Width-GetMetrix(ngmBorderWidth), Height),
        Rect(GetMetrix(ngmBorderWidth), 57, 62, 57 + GetMetrix(ngmBorderWidth)));
      Window.Draw(Canvas, Width-GetMetrix(ngmBorderWidth), Height-GetMetrix(ngmBorderWidth), Rect(63, 57, 63 + GetMetrix(ngmBorderWidth), 57 + GetMetrix(ngmBorderWidth)));
      { Left }
      Window.Draw(Canvas, 0, GetMetrix(ngmCaptionHeight), Rect(0, 29, GetMetrix(ngmBorderWidth), 29 + GetMetrix(ngmBorderWidth)));
      Window.Draw(Canvas, Rect(0, GetMetrix(ngmCaptionHeight)+GetMetrix(ngmBorderWidth), GetMetrix(ngmBorderWidth), Height-GetMetrix(ngmBorderWidth)),
        Rect(0, 29+GetMetrix(ngmBorderWidth), GetMetrix(ngmBorderWidth), 57));
      { Right }
      Window.Draw(Canvas, Width-GetMetrix(ngmBorderWidth), GetMetrix(ngmCaptionHeight), Rect(66-GetMetrix(ngmBorderWidth), 29, 66, 29 + GetMetrix(ngmBorderWidth)));
      Window.Draw(Canvas, Rect(Width-GetMetrix(ngmBorderWidth), GetMetrix(ngmCaptionHeight)+GetMetrix(ngmBorderWidth), Width, Height-GetMetrix(ngmBorderWidth)),
        Rect(66-GetMetrix(ngmBorderWidth), 29+GetMetrix(ngmBorderWidth), 66, 57));
    end;
    kbsToolWindow: begin
      { Caption }
      Window.Draw(Canvas, 0, 0, Rect(66, 0, 66+GetMetrix(ngmCaptionMargin), GetMetrix(ngmSmCaptionHeight)));
      Window.Draw(Canvas, Rect(GetMetrix(ngmCaptionMargin), 0, Width-GetMetrix(ngmCaptionMargin), GetMetrix(ngmSmCaptionHeight)),
        Rect(66+GetMetrix(ngmCaptionMargin), 0, 131-GetMetrix(ngmCaptionMargin), GetMetrix(ngmSmCaptionHeight)));
      Window.Draw(Canvas, Width-GetMetrix(ngmCaptionMargin), 0, Rect(131-GetMetrix(ngmCaptionMargin), 0, 131, GetMetrix(ngmSmCaptionHeight)));
      { Bottom }
      Window.Draw(Canvas, 0, Height-GetMetrix(ngmSmBorderWidth), Rect(66, 49, 66+GetMetrix(ngmSmBorderWidth), 49 + GetMetrix(ngmSmBorderWidth)));
      Window.Draw(Canvas, Rect(GetMetrix(ngmSmBorderWidth), Height-GetMetrix(ngmSmBorderWidth), Width-GetMetrix(ngmSmBorderWidth), Height),
        Rect(66+GetMetrix(ngmSmBorderWidth), 49, 131-GetMetrix(ngmSmBorderWidth), 49 + GetMetrix(ngmSmBorderWidth)));
      Window.Draw(Canvas, Width-GetMetrix(ngmSmBorderWidth), Height-GetMetrix(ngmSmBorderWidth),
        Rect(130, 49, 130+GetMetrix(ngmSmBorderWidth), 49 + GetMetrix(ngmSmBorderWidth)));
      { Left }
      Window.Draw(Canvas, 0, GetMetrix(ngmSmCaptionHeight),
        Rect(66, GetMetrix(ngmSmCaptionHeight), 66+GetMetrix(ngmSmBorderWidth), GetMetrix(ngmSmCaptionHeight) + GetMetrix(ngmSmBorderWidth)));
      Window.Draw(Canvas, Rect(0, GetMetrix(ngmSmCaptionHeight)+GetMetrix(ngmSmBorderWidth), GetMetrix(ngmSmBorderWidth), Height-GetMetrix(ngmSmBorderWidth)),
        Rect(66, GetMetrix(ngmSmCaptionHeight)+GetMetrix(ngmBorderWidth), 66+GetMetrix(ngmBorderWidth), 49));
      { Right }
      Window.Draw(Canvas, Width-GetMetrix(ngmSmBorderWidth), GetMetrix(ngmSmCaptionHeight),
        Rect(130, GetMetrix(ngmSmCaptionHeight), 130+GetMetrix(ngmSmBorderWidth), GetMetrix(ngmSmCaptionHeight) + GetMetrix(ngmSmBorderWidth)));
      Window.Draw(Canvas, Rect(Width-GetMetrix(ngmSmBorderWidth), GetMetrix(ngmSmCaptionHeight)+GetMetrix(ngmSmBorderWidth), Width, Height-GetMetrix(ngmSmBorderWidth)),
        Rect(130, GetMetrix(ngmSmCaptionHeight)+GetMetrix(ngmBorderWidth), 130+GetMetrix(ngmSmBorderWidth), 49));
    end;
  end;
end;

{ Menus Draw ==================================================================}

procedure TTeThemeLuna.CalcMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; var AWidth, AHeight: integer);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

  AWidth := Canvas.TextWidth(Item.Caption) + 12;
  AHeight := GetMetrix(ngmMenuBarHeight);
end;

procedure TTeThemeLuna.CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
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

procedure TTeThemeLuna.DrawMenuBar(Canvas: TCanvas; Width,
  Height: integer);
begin
  FillRect(Canvas, Rect(0, 0, Width, Height), GetColor(ngcMenuBar));
end;

procedure TTeThemeLuna.DrawPopupMenu(Canvas: TCanvas; Width,
  Height: integer);
var
  R: TRect;
begin
  R := Rect(0, 0, Width, Height);
  DrawEdge(Canvas, R, GetColor(ngcMenuBorder), GetColor(ngcMenuBorder));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, GetColor(ngcMenuItem), GetColor(ngcMenuItem));
  InflateRect(R, -1, -1);
  DrawEdge(Canvas, R, GetColor(ngcMenuItem), GetColor(ngcMenuItem));
end;

procedure TTeThemeLuna.DrawMenuBarItem(Canvas: TCanvas;
  Item: TTeCustomItem; Rect: TRect; Active, Hover: boolean);
begin
  Canvas.Font.Name := GetFontName(ngfMenuBarText);
  Canvas.Font.Size := GetFontSize(ngfMenuBarText);
  Canvas.Font.Style := GetFontStyle(ngfMenuBarText);

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
        DrawSpeedButton(Canvas, Rect, ngsNormal, false, false)
      else
        DrawSpeedButton(Canvas, Rect, ngsPressed, false, false);
      Canvas.Font.Color := GetColor(ngcMenuBarText);
    end
    else
    begin
      FillRect(Canvas, Rect, GetColor(ngcMenuBarHighlight));
      Canvas.Font.Color := GetColor(ngcMenuBarHighlightText);
      { Draw Border }
      DrawRect(Canvas, Rect, GetColor(ngcMenuBorder));
    end;
  end
  else
    Canvas.Font.Color := GetColor(ngcMenuBarText);

  { Text }
  DrawText(Canvas, Item.Caption, Rect, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE));
end;

procedure TTeThemeLuna.DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
var
  R, SrcRect: TRect;
begin
  case Item.MDIItemKind of
    mikSysMenu: begin
      if Active then
        if Hover then
          FillRect(Canvas, Rect, GetColor(ngcMenuBarHighlight))
        else
          FillRect(Canvas, Rect, GetColor(ngcMenuBarHighlight))
      else
        FillRect(Canvas, Rect, GetColor(ngcMenuBar));

      Exit;
    end;
    mikClose: SrcRect := Classes.Rect(38, 310, 57, 329);
    mikRestore: SrcRect := Classes.Rect(19, 310, 38, 329);
    mikMinimize: SrcRect := Classes.Rect(0, 310, 19, 329);
  end;

  if Active then
    if Hover then
      OffsetRect(SrcRect, 0, 19)
    else
      OffsetRect(SrcRect, 0, 38);
  { Draw }
  R := Classes.Rect(0, 0, 19, 19);
  RectCenter(R, Classes.Rect(0, 0, RectWidth(Rect), RectHeight(Rect)));
  OffsetRect(R, Rect.Left, Rect.Top);
  ActiveWindow.Draw(Canvas, R, SrcRect);
end;

procedure TTeThemeLuna.DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
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
  S: WideString;
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
    DrawFrameControlGlyph(Canvas, R, DFC_MENU, DFCS_MENUARROW, clBlack);
  end;
end;

procedure TTeThemeLuna.DrawMenuScrollButton(Canvas: TCanvas; Rect: TRect;
  Button: TTeMenuScrollButton; Active: boolean);
var
  R: TRect;
begin
  Canvas.Font.Color := GetColor(ngcMenuItemText);

  if Active then
  begin
    FillRect(Canvas, Rect, GetColor(ngcMenuItem));
    DrawEdge(Canvas, Rect, SunkenColor(GetColor(ngcMenuItem), 60), RaisedColor(GetColor(ngcMenuItem), 60));
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

procedure TTeThemeLuna.DrawButton(Canvas: TCanvas; Width, Height: integer;
  State: TTeThemeButtonState);
const
  Margin = 4;
var
  SrcRect: TRect;
begin
  case State of
    ngsHot: SrcRect := Rect(70, 130, 143, 151);
    ngsPressed: SrcRect := Rect(70, 160, 143, 181);
    ngsDisabled: SrcRect := Rect(70, 190, 143, 211);
    ngsFocused, ngsDefault: SrcRect := Rect(70, 220, 143, 241);
  else
    SrcRect := Rect(70, 100, 143, 121);
  end;
  { Draw }
  with SrcRect do
  begin
    { Draw border }
    ActiveWindow.Draw(Canvas, Rect(Margin, 0, Width-Margin, Margin),
      Rect(Left + Margin, Top, Right - Margin, Top + Margin));
    ActiveWindow.Draw(Canvas, Rect(Margin, Height-Margin, Width-Margin, Height),
      Rect(Left + Margin, Bottom-Margin, Right - Margin, Bottom));
    ActiveWindow.Draw(Canvas, Rect(0, Margin, Margin, Height-Margin),
      Rect(Left, Top + Margin, Left + Margin, Bottom - Margin));
    ActiveWindow.Draw(Canvas, Rect(Width - Margin, Margin, Width, Height-Margin),
      Rect(Right - Margin, Top + Margin, Right, Bottom - Margin));
    { Draw angles }
    ActiveWindow.Draw(Canvas, 0, 0, Rect(Left, Top, Left + Margin, Top + Margin), true);
    ActiveWindow.Draw(Canvas, Width-Margin, 0, Rect(Right-Margin, Top, Right, Top + Margin), true);
    ActiveWindow.Draw(Canvas, 0, Height-Margin, Rect(Left, Bottom-Margin, Left + Margin, Bottom), true);
    ActiveWindow.Draw(Canvas, Width-Margin, Height-Margin, Rect(Right-Margin, Bottom-Margin, Right, Bottom), true);
    { Draw Face }
    ActiveWindow.Draw(Canvas, Rect(Margin, Margin, Width-Margin, Height-Margin),
      Rect(Left + Margin, Top + Margin, Right- Margin, Bottom - Margin));
  end;
end;

procedure TTeThemeLuna.DrawCheckBox(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  SrcRect: TRect;
begin
  case ButtonState of
    ngsNormal: SrcRect := Classes.Rect(171, 102, 0, 0);
    ngsHot: SrcRect := Classes.Rect(171, 118, 0, 0);
    ngsPressed: SrcRect := Classes.Rect(171, 134, 0, 0);
    ngsDisabled: SrcRect := Classes.Rect(171, 150, 0, 0);
  end;
  case CheckState of
    ngcChecked: OffsetRect(SrcRect, 0, 64);
    ngcMixed: OffsetRect(SrcRect, 0, 128);
  end;

  SrcRect.Right := SrcRect.Left + GetMetrix(ngmCheckBoxWidth);
  SrcRect.Bottom := SrcRect.Top + GetMetrix(ngmCheckBoxHeight);
  { Draw }
  ActiveWindow.Draw(Canvas, Rect.Left, Rect.Top, SrcRect, true);
end;

procedure TTeThemeLuna.DrawRadioButton(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
var
  SrcRect: TRect;
begin
  case ButtonState of
    ngsHot: SrcRect := Classes.Rect(152, 118, 165, 131);
    ngsPressed: SrcRect := Classes.Rect(152, 134, 165, 147);
    ngsDisabled: SrcRect := Classes.Rect(152, 150, 165, 163);
  else
    SrcRect := Classes.Rect(152, 102, 165, 115);
  end;
  if CheckState = ngcChecked then
     OffsetRect(SrcRect, 0, 64);

  SrcRect.Right := SrcRect.Left + GetMetrix(ngmCheckBoxWidth);
  SrcRect.Bottom := SrcRect.Top + GetMetrix(ngmCheckBoxHeight);

  { Draw }
  ActiveWindow.Draw(Canvas, Rect.Left, Rect.Top, SrcRect, true);
end;

procedure TTeThemeLuna.DrawTrackBarThumb(Canvas: TCanvas; R: TRect;
  Orientation: TTrackOrientation; TickMarks: TTickMark;
  State: TTeThemeButtonState);
var
  SrcRect: TRect;
  STeThemep: integer;
begin
  case State of
    ngsHot: STeThemep := 1;
    ngsPressed: STeThemep := 2;
    ngsFocused: STeThemep := 3;
    ngsDisabled: STeThemep := 4;
  else
    STeThemep := 0;
  end;

  if Orientation = toHorizontal then
  begin
    case TickMarks of
      tmBoth: begin
        SrcRect := Rect(80, 100, 91, 123);
        OffsetRect(SrcRect, 0, 43 * STeThemep);
      end;
      tmBottomRight: begin
        SrcRect := Rect(129, 96, 140, 118);
        OffsetRect(SrcRect, 0, 55 * STeThemep);
      end;
      tmTopLeft: begin
        SrcRect := Rect(129, 67, 140, 89);
        OffsetRect(SrcRect, 0, 55 * STeThemep);
      end;
    end;
  end
  else
  begin
    case TickMarks of
      tmBoth: begin
        SrcRect := Rect(75, 128, 97, 139);
        OffsetRect(SrcRect, 0, 43 * STeThemep);
      end;
      tmBottomRight: begin
        SrcRect := Rect(153, 96, 175, 107);
        OffsetRect(SrcRect, 0, 55 * STeThemep);
      end;
      tmTopLeft: begin
        SrcRect := Rect(152, 78, 174, 89);
        OffsetRect(SrcRect, 0, 55 * STeThemep);
      end;
    end;
  end;

  { Draw }
  InactiveWindow.Draw(Canvas, R.Left, R.Top, SrcRect, true);
end;

procedure TTeThemeLuna.DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect);
begin
  FillRect(Canvas, R, GetColor(ngcWindow));
  DrawEdge(Canvas, R, clBtnShadow, clBtnHighlight);

  if not IsRectEmpty(HighlightR) then
  begin
    InflateRect(HighlightR, -1, -1);
    FillRect(Canvas, HighlightR, RGB(33, 195, 33));
  end;
end;

procedure TTeThemeLuna.DrawProgessFrame(Canvas: TCanvas; R: TRect);
var
  BackColor: TColor;
  BorderColor: TColor;
begin
  BackColor := GetColor(ngcWindow);
  BorderColor := GetColor(ngcBorder);

  DrawRoundRect(Canvas, R, 3, BorderColor);
  InflateRect(R, -1, -1);
  FillRect(Canvas, R, BackColor);
end;

procedure TTeThemeLuna.DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
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

procedure TTeThemeLuna.DrawPanel(Canvas: TCanvas; R: TRect; ShowBevel, ShowCaption: boolean);
begin
  { Draw Panel Face }
  FillRoundRect(Canvas, R, 6, GetColor(ngcWindow));
  DrawRoundRect(Canvas, R, 6, GetColor(ngcBorder));

  Inc(R.Top, 20);
  DrawRect(Canvas, R, GetColor(ngcBorder));
  InflateRect(R, -1, -1);
  Dec(R.Top, 3);
  FillRect(Canvas, R, GetColor(ngcWindow));
end;

procedure TTeThemeLuna.DrawPanelCaption(Canvas: TCanvas; R: TRect;
  Caption: WideString);
var
  R1: TRect;
begin
  { Draw line }
  MoveTo(Canvas, R.Left, R.Bottom);
  LineTo(Canvas, R.Right, R.Bottom, GetColor(ngcBorder));

  { Draw round grad }
  R1 := R; R1.Bottom := R1.Top + 1;
  InflateRect(R1, -1, 0);
  FillGradientRect(Canvas, R1, GetColor(ngcPanelCaption),
    GetColor(ngcPanelCaption2), false);
  InflateRect(R1, 1, 0);
  OffsetRect(R1, 0, 1);
  FillGradientRect(Canvas, R1, GetColor(ngcPanelCaption),
    GetColor(ngcPanelCaption2), false);

  R.Top := R1.Bottom;
  FillGradientRect(Canvas, R, GetColor(ngcPanelCaption),
    GetColor(ngcPanelCaption2), false);

  { Draw Text }
  InflateRect(R, -5, 0);
  Canvas.Font.Color := GetColor(ngcWindowText);
  Canvas.Font.Name := GetFontName(ngfWindowText);
  Canvas.Font.Size := GetFontSize(ngfWindowText);
  Canvas.Font.Style:= GetFontStyle(ngfWindowText);
  DrawText(Canvas, Caption, R, DrawTextBiDiModeFlags(DT_LEFT or DT_SINGLELINE or DT_VCenter));
end;

procedure TTeThemeLuna.DrawPanelButton(Canvas: TCanvas; R: TRect;
  Kind: TTePanelButtonKind; State: TTeThemeButtonState; Rolled: boolean);
var
  SrcRect: TRect;
begin
  if State = ngsPressed then
    SrcRect := Rect(168, 32, 185, 49)
  else
    if State = ngsHot then
      SrcRect := Rect(151, 32, 168, 49)
    else
      SrcRect := Rect(134, 32, 151, 49);

  case Kind of
    pbkRoll: if Rolled then OffsetRect(SrcRect, 0, 17);
    pbkHide: OffsetRect(SrcRect, 0, 34);
  end;

  ActiveWindow.Draw(Canvas, R.Left, R.Top, SrcRect, true);
end;

procedure TTeThemeLuna.DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect;
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


procedure TTeThemeLuna.DrawScrollBar(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind);
var
  SrcRect: TRect;
begin
  if Kind = sbHorizontal then
    SrcRect := Rect(19, 305, 36, 322)
  else
    SrcRect := Rect(37, 305, 54, 322);

  InactiveWindow.Draw(Canvas, R, SrcRect);
end;

procedure TTeThemeLuna.DrawScrollBarButton(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; LeftTop: boolean; State: TTeThemeButtonState);
var
  SrcRect: TRect;
begin
  DrawScrollBarSlider(Canvas, R, Kind, State);

  { Draw Glyph }
  SrcRect := Rect(2, 375, 11, 384);
  if State = ngsDisabled then
    OffsetRect(SrcRect, 0, 9 * 3);
  if State = ngsPressed then
    OffsetRect(SrcRect, 0, 9 * 2);
  if State = ngsHot then
    OffsetRect(SrcRect, 0, 9);

  if Kind = sbHorizontal then
    if LeftTop then 
      OffsetRect(SrcRect, 9, 0)
    else
      OffsetRect(SrcRect, 9 * 3, 0);

  if Kind = sbVertical then
    if not LeftTop then
      OffsetRect(SrcRect, 9 * 2, 0);

  InactiveWindow.Draw(Canvas, R.Left + (RectWidth(R) - 9) div 2,
    R.Top + (RectHeight(R) - 9) div 2, SrcRect, true);
end;

procedure TTeThemeLuna.DrawScrollBarSlider(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; State: TTeThemeButtonState);
var
  SrcRect: TRect;
begin
  if State = ngsDisabled then
    SrcRect := Rect(1, 356, 18, 373)
  else
    if State = ngsPressed then
      SrcRect := Rect(1, 339, 18, 356)
    else
      if State = ngsHot then
        SrcRect := Rect(1, 322, 18, 339)
      else
        SrcRect := Rect(1, 305, 18, 322);

  InactiveWindow.DrawMargin(Canvas, R, SrcRect, 3, 3, 3, 3, true);
end;

procedure TTeThemeLuna.DrawTabBorder(Canvas: TCanvas; R: TRect);
const
  Margin = 4;
var
  SrcRect: TRect;
begin
  SrcRect := Rect(153, 344, 168, 362);
  { Draw }
  with SrcRect do
  begin
    { Draw border }
    InactiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Top, R.Right-Margin, R.Top + Margin),
      Rect(Left + Margin, Top, Right - Margin, Top + Margin));
    InactiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Bottom-Margin, R.Right-Margin, R.Bottom),
      Rect(Left + Margin, Bottom-Margin, Right - Margin, Bottom));
    InactiveWindow.Draw(Canvas, Rect(R.Left, R.Top + Margin, R.Left + Margin, R.Bottom-Margin),
      Rect(Left, Top + Margin, Left + Margin, Bottom - Margin));
    InactiveWindow.Draw(Canvas, Rect(R.Right - Margin, R.Top + Margin, R.Right, R.Bottom-Margin),
      Rect(Right - Margin, Top + Margin, Right, Bottom - Margin));
    { Draw angles }
    InactiveWindow.Draw(Canvas, R.Left, R.Top, Rect(Left, Top, Left + Margin, Top + Margin));
    InactiveWindow.Draw(Canvas, R.Right-Margin, R.Top, Rect(Right-Margin, Top, Right, Top + Margin));
    InactiveWindow.Draw(Canvas, R.Left, R.Bottom-Margin, Rect(Left, Bottom-Margin, Left + Margin, Bottom));
    InactiveWindow.Draw(Canvas, R.Right-Margin, R.Bottom-Margin, Rect(Right-Margin, Bottom-Margin, Right, Bottom));
    { Draw Face }
    InactiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Top + Margin, R.Right-Margin, R.Bottom-Margin),
      Rect(Left + Margin, Top + Margin, Right- Margin, Bottom - Margin));
  end;
end;

procedure TTeThemeLuna.DrawTab(Canvas: TCanvas; R: TRect; TabPosition: TTabPosition; State: TTeThemeButtonState);
const
  Margin = 4;
var
  SrcRect: TRect;
begin
  case State of
    ngsHot: SrcRect := Rect(68, 344, 89, 365);
    ngsNormal: SrcRect := Rect(68, 365, 89, 386);
    ngsFocused: SrcRect := Rect(69, 386, 87, 407);
  end;

  case TabPosition of
    tpBottom: OffsetRect(SrcRect, 21, 0);
    tpRight: OffsetRect(SrcRect, 42, 0);
    tpLeft: OffsetRect(SrcRect, 63, 0);
  end;
  
  { Draw }
  with SrcRect do
  begin
    { Draw border }
    InactiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Top, R.Right-Margin, R.Top + Margin),
      Rect(Left + Margin, Top, Right - Margin, Top + Margin));
    InactiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Bottom-Margin, R.Right-Margin, R.Bottom),
      Rect(Left + Margin, Bottom-Margin, Right - Margin, Bottom));
    InactiveWindow.Draw(Canvas, Rect(R.Left, R.Top + Margin, R.Left + Margin, R.Bottom-Margin),
      Rect(Left, Top + Margin, Left + Margin, Bottom - Margin));
    InactiveWindow.Draw(Canvas, Rect(R.Right - Margin, R.Top + Margin, R.Right, R.Bottom-Margin),
      Rect(Right - Margin, Top + Margin, Right, Bottom - Margin));
    { Draw angles }
    InactiveWindow.Draw(Canvas, R.Left, R.Top, Rect(Left, Top, Left + Margin, Top + Margin), true);
    InactiveWindow.Draw(Canvas, R.Right-Margin, R.Top, Rect(Right-Margin, Top, Right, Top + Margin), true);
    InactiveWindow.Draw(Canvas, R.Left, R.Bottom-Margin, Rect(Left, Bottom-Margin, Left + Margin, Bottom), true);
    InactiveWindow.Draw(Canvas, R.Right-Margin, R.Bottom-Margin, Rect(Right-Margin, Bottom-Margin, Right, Bottom), true);
    { Draw Face }
    InactiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Top + Margin, R.Right-Margin, R.Bottom-Margin),
      Rect(Left + Margin, Top + Margin, Right- Margin, Bottom - Margin));
  end;
end;

procedure TTeThemeLuna.DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
  TabPosition: TTabPosition; State: TTeThemeButtonState);
begin
  DrawScrollBarButton(Canvas, R, sbHorizontal, LeftTop, State);
end;

procedure TTeThemeLuna.DrawControlFrame(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState);
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
  InflateRect(R, -1, -1);
  DrawRect(Canvas, R, Color);
end;

procedure TTeThemeLuna.DrawComboButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState);
var
  MarginR, SrcRect: TRect;
begin
  SrcRect := Rect(151, 248, 166, 262);

  if State = ngsDisabled then
    OffsetRect(SrcRect, 0, 3 * 14);
  if State = ngsPressed then
    OffsetRect(SrcRect, 0, 2 * 14);
  if State = ngsHot then
    OffsetRect(SrcRect, 0, 14);

  { Draw Button }
  { Top Margin }
  MarginR := SrcRect;
  MarginR.Bottom := MarginR.Top + 3;
  ActiveWindow.Draw(Canvas, R.Left, R.Top, MarginR);
  { Bottom Margin }
  MarginR := SrcRect;
  MarginR.Top := MarginR.Bottom - 3;
  ActiveWindow.Draw(Canvas, R.Left, R.Bottom - 3, MarginR);

  InflateRect(SrcRect, 0, -3);
  InflateRect(R, 0, -3);
  ActiveWindow.Draw(Canvas, R, SrcRect);

  { Draw Glyph }
  SrcRect := Rect(155, 314, 165, 321);

  if State = ngsDisabled then
    OffsetRect(SrcRect, 0, 3 * 7);
  if State = ngsPressed then
    OffsetRect(SrcRect, 0, 2 * 7);
  if State = ngsHot then
    OffsetRect(SrcRect, 0, 7);

  ActiveWindow.Draw(Canvas, R.Left + (RectWidth(R) - RectWidth(SrcRect)) div 2,
    R.Top + (RectHeight(R) - RectHeight(SrcRect)) div 2, SrcRect, true);
end;

procedure TTeThemeLuna.DrawSpeedButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
const
  Margin = 4;
var
  SrcRect: TRect;
begin
  if Flat and (State = ngsNormal) and not Exclusive then Exit;
  if State = ngsDisabled then Exit;

  SrcRect := Rect(69, 278, 89, 301);

  if not Flat then
  begin
    { Standard }
    case State of
      ngsNormal:
        if not Exclusive then
          OffsetRect(SrcRect, 20, 0)
        else
          OffsetRect(SrcRect, 20, 23);
      ngsPressed:
        if not Exclusive then
          OffsetRect(SrcRect, 40, 0)
        else
          OffsetRect(SrcRect, 20, 23);
      ngsDisabled: OffsetRect(SrcRect, 0, 23);
    end;
  end
  else
  begin
    { Flat }
    if State = ngsDisabled then
      OffsetRect(SrcRect, 0, 23)
    else
    begin
      if State = ngsPressed then
        OffsetRect(SrcRect, 40, 0)
      else
        if State = ngsHot then
          if not Exclusive then
            OffsetRect(SrcRect, 20, 0)
          else
            OffsetRect(SrcRect, 40, 23);
    end;

    if Exclusive and (State = ngsNormal) then
      OffsetRect(SrcRect, 20, 23);

  end;

  { Draw }
  with SrcRect do
  begin
    { Draw border }
    ActiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Top, R.Right-Margin, R.Top + Margin),
      Rect(Left + Margin, Top, Right - Margin, Top + Margin));
    ActiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Bottom-Margin, R.Right-Margin, R.Bottom),
      Rect(Left + Margin, Bottom-Margin, Right - Margin, Bottom));
    ActiveWindow.Draw(Canvas, Rect(R.Left, R.Top + Margin, R.Left + Margin, R.Bottom-Margin),
      Rect(Left, Top + Margin, Left + Margin, Bottom - Margin));
    ActiveWindow.Draw(Canvas, Rect(R.Right - Margin, R.Top + Margin, R.Right, R.Bottom-Margin),
      Rect(Right - Margin, Top + Margin, Right, Bottom - Margin));
    { Draw angles }
    ActiveWindow.Draw(Canvas, R.Left, R.Top, Rect(Left, Top, Left + Margin, Top + Margin), true);
    ActiveWindow.Draw(Canvas, R.Right-Margin, R.Top, Rect(Right-Margin, Top, Right, Top + Margin), true);
    ActiveWindow.Draw(Canvas, R.Left, R.Bottom-Margin, Rect(Left, Bottom-Margin, Left + Margin, Bottom), true);
    ActiveWindow.Draw(Canvas, R.Right-Margin, R.Bottom-Margin, Rect(Right-Margin, Bottom-Margin, Right, Bottom), true);
    { Draw Face }
    ActiveWindow.Draw(Canvas, Rect(R.Left+Margin, R.Top+Margin, R.Right-Margin, R.Bottom-Margin),
      Rect(Left + Margin, Top + Margin, Right- Margin, Bottom - Margin));
  end;
end;

procedure TTeThemeLuna.DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
var
  SrcRect: TRect;
begin
  DrawSpeedButton(Canvas, R, State, Flat, Exclusive);

  { Draw Glyph }
  SrcRect := Rect(60, 311, 66, 314);

  if State = ngsDisabled then
    OffsetRect(SrcRect, 0, 3 * 3);
  if State = ngsPressed then
    OffsetRect(SrcRect, 0, 2 * 3);
  if State = ngsHot then
    OffsetRect(SrcRect, 0, 3);

  if Exclusive then
    OffsetRect(SrcRect, 0, 4 * 3);

  ActiveWindow.Draw(Canvas, R.Left + (RectWidth(R) - RectWidth(SrcRect)) div 2,
    R.Top + (RectHeight(R) - RectHeight(SrcRect)) div 2, SrcRect, true);
end;

procedure TTeThemeLuna.DrawSpinButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Up: boolean);
const
  Margin = 3;
var
  SrcRect: TRect;
begin
  SrcRect := Rect(132, 278, 146, 292);

  if State = ngsDisabled then
    OffsetRect(SrcRect, 0, 3 * 14)
  else
    if State = ngsPressed then
      OffsetRect(SrcRect, 0, 2 * 14)
    else
      if State = ngsHot then
        OffsetRect(SrcRect, 0, 14);

  { Draw }
  with SrcRect do
  begin
    { Draw border }
    ActiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Top, R.Right-Margin, R.Top + Margin),
      Rect(Left + Margin, Top, Right - Margin, Top + Margin));
    ActiveWindow.Draw(Canvas, Rect(R.Left + Margin, R.Bottom-Margin, R.Right-Margin, R.Bottom),
      Rect(Left + Margin, Bottom-Margin, Right - Margin, Bottom));
    ActiveWindow.Draw(Canvas, Rect(R.Left, R.Top + Margin, R.Left + Margin, R.Bottom-Margin),
      Rect(Left, Top + Margin, Left + Margin, Bottom - Margin));
    ActiveWindow.Draw(Canvas, Rect(R.Right - Margin, R.Top + Margin, R.Right, R.Bottom-Margin),
      Rect(Right - Margin, Top + Margin, Right, Bottom - Margin));
    { Draw angles }
    ActiveWindow.Draw(Canvas, R.Left, R.Top, Rect(Left, Top, Left + Margin, Top + Margin), true);
    ActiveWindow.Draw(Canvas, R.Right-Margin, R.Top, Rect(Right-Margin, Top, Right, Top + Margin), true);
    ActiveWindow.Draw(Canvas, R.Left, R.Bottom-Margin, Rect(Left, Bottom-Margin, Left + Margin, Bottom), true);
    ActiveWindow.Draw(Canvas, R.Right-Margin, R.Bottom-Margin, Rect(Right-Margin, Bottom-Margin, Right, Bottom), true);
    { Draw Face }
    ActiveWindow.Draw(Canvas, Rect(R.Left+Margin, R.Top+Margin, R.Right-Margin, R.Bottom-Margin),
      Rect(Left + Margin, Top + Margin, Right- Margin, Bottom - Margin));
  end;
end;

procedure TTeThemeLuna.DrawControlBar(Canvas: TCanvas; R: TRect);
begin
  { ControlBar Background }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeLuna.DrawControlBarFrame(Canvas: TCanvas; R, GrabberRect: TRect);
begin
  { ControlBar Frame }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
  DrawEdge(Canvas, R, RaisedColor(GetColor(ngcBtnFace), 50),
    SunkenColor(GetColor(ngcBtnFace), 50));

  { Draw Grabber }
  ActiveWindow.DrawTile(Canvas, GrabberRect, Rect(119, 58, 129, 64), true);
end;

procedure TTeThemeLuna.DrawToolbar(Canvas: TCanvas; R: TRect);
begin
  { Draw toolbar }
  FillRect(Canvas, R, GetColor(ngcBtnFace));
end;

procedure TTeThemeLuna.DrawGridCell(Canvas: TCanvas; R: TRect;
  State: TGridDrawState);
var
  Color: TColor;
begin
  { Draw grid cell }
  if gdFixed in State then
  begin
    InflateRect(R, 1, 1);
    FillRect(Canvas, R, GetColor(ngcBtnFace));
    DrawRect(Canvas, R, GetColor(ngcMenuItemHighlight));
  end
  else
  begin
    if gdFocused in State then
      Color := GetColor(ngcHighlight)
    else
      if gdSelected in State then
        Color := GetColor(ngcHotHighlight)
      else
        Color := GetColor(ngcWindow);

    FillRect(Canvas, R, Color);
  end;
end;

procedure TTeThemeLuna.DrawSplitter(Canvas: TCanvas; ARect: TRect; AHot, ABeveled: boolean);
begin
  if not AHot then
    FillRect(Canvas, ARect, GetColor(ngcBtnFace))
  else
    FillRect(Canvas, ARect, GetColor(ngcHotHighlight));
end;

procedure TTeThemeLuna.DrawHeaderSection(Canvas: TCanvas; ARect: TRect;
  Section: TTeHeaderSection; AState: TTeSectionState);
var
  Color, RColor, SColor: TColor;
  SrcRect: TRect;
begin
  { Section may be nil}
   
  SrcRect := Rect(103, 101, 121, 119);
  if AState = ssDraggedOut then
    OffsetRect(SrcRect, 0, 18 * 3)
  else
    if AState = ssUnderDrag then
      OffsetRect(SrcRect, 0, 18 * 2)
    else
      if AState in [ssPressed] then
        OffsetRect(SrcRect, 0, 18 * 2)
      else
        if AState in [ssHot, ssDraggin] then
          OffsetRect(SrcRect, 0, 18 * 1);

  InactiveWindow.DrawMargin(Canvas, ARect, SrcRect, 4, 4, 4, 4, true);
end;

procedure TTeThemeLuna.DrawStatusBar(Canvas: TCanvas; ARect: TRect);
begin
  ActiveWindow.Draw(Canvas, ARect, Rect(60, 331, 128, 346));
end;

procedure TTeThemeLuna.DrawStatusGripper(Canvas: TCanvas; ARect: TRect);
begin
  ActiveWindow.Draw(Canvas, ARect, Rect(69, 279, 85, 296), true);
end;

procedure TTeThemeLuna.DrawStatusPanel(Canvas: TCanvas; ARect: TRect;
  Panel: TTeStatusPanel);
var
  R: TRect;
begin
  if (Panel <> nil) and (Panel.StatusBar <> nil) then
  begin
    R := Rect(0, 0, Panel.StatusBar.Width, Panel.StatusBar.Height);
    ActiveWindow.Draw(Canvas, R, Rect(60, 331, 128, 346));
  end;
  ActiveWindow.Draw(Canvas, Rect(ARect.Right - 3, ARect.Top, ARect.Right, ARect.Bottom), Rect(60, 348, 63, 363), true);
end;

procedure TTeThemeLuna.DrawScrollBox(Canvas: TCanvas; R: TRect;
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

function TTeThemeLuna.GetRegion(Width, Height: integer;
  BorderStyle: TTeBorderStyle): HRgn;
var
  TeThememp: HRgn;
begin
  Result := CreateRectRgn(0, 0, Width, Height);

  if BorderStyle <> kbsStandard then Exit;

  { LeftTop }
  TeThememp := CreateRectRgn(0, 0, 5, 1);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  TeThememp := CreateRectRgn(0, 0, 3, 2);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  TeThememp := CreateRectRgn(0, 0, 2, 3);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  TeThememp := CreateRectRgn(0, 0, 1, 5);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  { RightTop }
  TeThememp := CreateRectRgn(Width-5, 0, Width, 1);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  TeThememp := CreateRectRgn(Width-3, 0, Width, 2);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  TeThememp := CreateRectRgn(Width-2, 0, Width, 3);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
  TeThememp := CreateRectRgn(Width-1, 0, Width, 5);
  CombineRgn(Result, Result, TeThememp, RGN_DIFF);
  DeleteObject(TeThememp);
end;

initialization
  RegisterTheme(TTeThemeLuna);
end.




