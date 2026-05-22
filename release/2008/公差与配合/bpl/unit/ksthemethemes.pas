{==============================================================================

  Base Themes
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemethemes.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemethemes;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus, Grids,
  te_controls;

type


  { GUI metrics. The following table lists the possible values:
    <TABLE>
    Value                       Meaning
    -----                       -------
    ngmBorderWidth              Specifies the form border width
    ngmSmBorderWidth            Specifies the form border width for ToolWindoiw
    ngmCaptionHeight            Specifies the title bar height
    ngmSmCaptionHeight          Specifies the title bar height for ToolWindow
    ngmCaptionMargin            Specifies the top and left caption margin
    ngmButtonWidth              Specifies the System button width
    ngmButtonHeight             Specifies the System button height
    ngmSmButtonWidth,           Specifies the System button width for ToolWindow
    ngmSmButtonHeight           Specifies the System button height for ToolWindow
    ngmButtonMarginX            Specifies the left or right margin for System button
    ngmButtonMarginY            Specifies the top or bottom margin for System button
    ngmButtonSpace              Specifies the space between System button's
    ngmSmButtonMarginX          Specifies the left or right margin for System button for ToolWindow
    ngmSmButtonMarginY          Specifies the top or bottom margin for System button for ToolWindow
    ngmMenuBarHeight            Specifies the menubar height
    ngmMenuItemHeight           Specifies the menuItem height
    ngmCheckBoxWidth            Specifies the checkbox box width
    ngmCheckBoxHeight           Specifies the checkbox box height
    ngmRadioButtonWidth         Specifies the radiobutton box width
    ngmRadioButtonHeight        Specifies the radiobutton box height
    ngmTrackThumbWidth          Specifies the trackbar slider width ( or height for vertical orientation )
    ngmTrackThumbHeight         Specifies the trackbar slider height ( or width for vertical orientation )
    ngmTrackBarHeight           Specifies the trackbar height ( or width for vertical orientation )
    ngmPanelCaptionHeight       Specifies the panel caption's height
    ngmScrollBarHeight          Specifies the scrollbar height
    ngmSliderWidth              Specifies the scrollbar slider width
    ngmTabMargin                Specifies the TabControl margins
    ngmTabHeight                Specifies the tab height
    </TABLE>
  }
  TTeThemeMetrix = (
    { Forms }
    ngmBorderWidth, ngmSmBorderWidth, ngmCaptionHeight, ngmSmCaptionHeight,
    ngmCaptionMargin, ngmButtonWidth, ngmButtonHeight, ngmSmButtonWidth,
    ngmSmButtonHeight, ngmButtonMarginX, ngmButtonMarginY, ngmButtonSpace,
    ngmSmButtonMarginX, ngmSmButtonMarginY,
    { Menus }
    ngmMenuBarHeight, ngmMenuItemHeight,
    { Constols }
    ngmCheckBoxWidth, ngmCheckBoxHeight,
    ngmRadioButtonWidth, ngmRadioButtonHeight,
    ngmTrackThumbWidth, ngmTrackThumbHeight, ngmTrackBarHeight,
    ngmPanelCaptionHeight, ngmScrollBarHeight, ngmSliderWidth,
    ngmTabMargin, ngmTabHeight, ngmComboButtonWidth,
    ngmGrabberSize
  );

  { Set of theme's colors }
  TTeThemeColor = (
    { Forms }
    ngcCaption, ngcInactiveCaption, ngcCaptionShadow,
    ngcCaptionText, ngcSmCaptionText, ngcInactiveCaptionText,
    ngcWindow, ngcBorder, ngcHighlight, ngcDisabled, ngcDisabledBorder,
    ngcBtnFace, ngcHotHighlight, 
    { Menus }
    ngcMenuBorder, ngcMenuBar, ngcMenuBarHighlight, ngcMenuItem, ngcMenuItemHighlight,
    ngcMenuBarText, ngcMenuBarHighlightText, ngcMenuItemText,
    ngcMenuItemHighlightText, ngcMenuItemDisabledText,
    { Controls }
    ngcWindowText,
    ngcPanelCaption, ngcPanelCaption2
  );

  { Set of theme's fonts }
  TTeThemeFont = (
    { Forms }
    ngfCaptionText, ngfSmCaptionText,
    { Menus }
    ngfMenuBarText, ngfMenuItemText,
    { Controls }
    ngfWindowText
  );

  { The following table lists the possible values:
    <TABLE>
    Value                       Meaning
    -----                       -------
    ngbClose                    Close button
    ngbHelp                     Help button
    ngbMin                      Minimize button
    ngbMax                      Maximize button
    ngbRestore                  Restore button
    ngbRollup,                  Rollup button
    ngbRolldown                 Rolldown button
    ngbTray                     Minimize to tray button
    </TABLE>
  }
  TTeThemeSysButton = (ngbClose, ngbHelp, ngbMin, ngbMax, ngbRestore, ngbRollup,
    ngbRolldown, ngbTray);

  { The following table lists the possible values:
    <TABLE>
    Value                       Meaning
    -----                       -------
    ngsNormal                   Normal State
    ngsHot                      Rollover State
    ngsPressed                  Pressed State
    ngsDisabled                 Disabled State
    ngsFocused                  Focused State
    ngsDefault                  Default (for button only) State
    </TABLE>
  }
  TTeThemeButtonState = (ngsNormal, ngsHot, ngsPressed, ngsDisabled, ngsFocused, ngsDefault);

  { The following table lists the possible values:
    <TABLE>
    Value                       Meaning
    -----                       -------
    ngcChecked                  Checked State
    ngcUnChecked                Unchecked State
    ngcMixed                    Mixed (grayed) State
    </TABLE>
  }
  TTeThemeCheckBoxState = (ngcUnChecked, ngcChecked, ngcMixed);

{ TTeTheme class }

{ Main theme's support class. Specifies the appearance and behavior by using specified methods. }
  TTeTheme = class(TPersistent)
  private
    FDeltaHue: integer;
    FDeltaBrightness: integer;
    FBiDiMode: TBiDiMode;
    procedure SetDeltaHue(const Value: integer);
    procedure SetDeltaBrightness(const Value: integer);
    procedure SetBiDiMode(const Value: TBiDiMode);
    function UseRightToLeftAlignment: Boolean;
    function UseRightToLeftReading: Boolean;
    function UseRightToLeftScrollBar: Boolean;
  protected
    procedure ChangeThemeColors; virtual;
    function DrawTextBiDiModeFlags(Flags: integer): integer;
    function DrawTextBiDiModeFlagsReadingOnly: Longint;
  public
    constructor Create; virtual;
    destructor Destroy; override;
    { Return GUI metrics
      See Also:
        TTeThemeMetrix
    }
    function GetMetrix(Metrix: TTeThemeMetrix): integer; virtual; 
    { Return colors }
    function GetColor(Color: TTeThemeColor): TColor; virtual; 
    { Return font names }
    function GetFontName(Font: TTeThemeFont): string; virtual; 
    { Return font sizes }
    function GetFontSize(Font: TTeThemeFont): integer; virtual; 
    { Return font styles }
    function GetFontStyle(Font: TTeThemeFont): TFontStyles; virtual;
    { Return cption font effect }
    function HasCaptionTextShadow: boolean; virtual;  

    { The System button drawing }
    procedure DrawSysButton(Canvas: TCanvas; R: TRect;
      Kind: TTeThemeSysButton; Hot, Down, Active: boolean; BorderStyle: TTeBorderStyle); virtual;
    { The form drawing }
    procedure DrawWindow(Canvas: TCanvas; Width, Height: integer;
      ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle); virtual; 
    { Calc menubar Item width and height }
    procedure CalcMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem; var AWidth, AHeight: integer); virtual; 
    { Calc popup menu Item width and height }
    procedure CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem; var AWidth, AHeight: integer); virtual; 
    { The menubar drawing  }
    procedure DrawMenuBar(Canvas: TCanvas; Width, Height: integer); virtual;
    { The menubar Item drawing  }
    procedure DrawMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); virtual; 
    { The menubar MDI icons drawing  }
    procedure DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); virtual;
    { The popupmenu drawing  }
    procedure DrawPopupMenu(Canvas: TCanvas; Width, Height: integer); virtual; 
    { The popupmenu Item drawing  }
    procedure DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); virtual;
    { The popupmenu scroll button  }
    procedure DrawMenuScrollButton(Canvas: TCanvas; Rect: TRect;
      Button: TTeMenuScrollButton; Active: boolean); virtual;

    { The button drawing  }
    procedure DrawButton(Canvas: TCanvas; Width, Height: integer;
      State: TTeThemeButtonState); virtual; 

    { The checkbox drawing  }
    procedure DrawCheckBox(Canvas: TCanvas; Rect: TRect;
      ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState); virtual;
    { The radiobutton drawing  }
    procedure DrawRadioButton(Canvas: TCanvas; Rect: TRect;
      ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState); virtual; 

    { The trackbar slider drawing  }
    procedure DrawTrackBarThumb(Canvas: TCanvas; R: TRect; Orientation: TTrackOrientation;
      TickMarks: TTickMark; State: TTeThemeButtonState); virtual; 
    { The trackbar background drawing  }
    procedure DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect); virtual; 

    { The progressbar frame drawing  }
    procedure DrawProgessFrame(Canvas: TCanvas; R: TRect); virtual; 
    { The progressbar bar drawing  }
    procedure DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
      Orientation: TTeBarOrientation; Smooth: boolean); virtual; 

    { The panel background and frame drawing  }
    procedure DrawPanel(Canvas: TCanvas; R: TRect; ShowBevel, ShowCaption: boolean); virtual; 
    { The panel caption's drawing  }
    procedure DrawPanelCaption(Canvas: TCanvas; R: TRect; Caption: WideString); virtual;
    { The panel button's drawing  }
    procedure DrawPanelButton(Canvas: TCanvas;  R: TRect; Kind: TTePanelButtonKind;
      State: TTeThemeButtonState; Rolled: boolean); virtual; 

    { The groupbox drawing  }
    procedure DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect; Caption: WideString); virtual; 

    { The scrollbar drawing  }
    procedure DrawScrollBar(Canvas: TCanvas; R: TRect; Kind: TScrollBarKind); virtual;
    { The scrollbar buttons drawing  }
    procedure DrawScrollBarButton(Canvas: TCanvas; R: TRect; Kind: TScrollBarKind;
      LeftTop: boolean; State: TTeThemeButtonState); virtual; 
    { The scrollbar slider drawing  }
    procedure DrawScrollBarSlider(Canvas: TCanvas; R: TRect; Kind: TScrollBarKind;
      State: TTeThemeButtonState); virtual;

    { The TabControl border drawing }
    procedure DrawTabBorder(Canvas: TCanvas; R: TRect); virtual;
    { The tab drawing }
    procedure DrawTab(Canvas: TCanvas; R: TRect; TabPosition: TTabPosition;
      State: TTeThemeButtonState); virtual;
    { The tab scroll button }
    procedure DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
      TabPosition: TTabPosition; State: TTeThemeButtonState); virtual;

    { Controls }
    procedure DrawControlFrame(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState); virtual;

    { ComboBox }
    procedure DrawComboButton(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState); virtual;

    { SpeedButtons }
    procedure DrawSpeedButton(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState;
      Flat, Exclusive: boolean); virtual;
    procedure DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState;
      Flat, Exclusive: boolean); virtual;

    { ControlBar }
    procedure DrawControlBar(Canvas: TCanvas; R: TRect); virtual;
    procedure DrawControlBarFrame(Canvas: TCanvas; R, GrabberRect: TRect); virtual;

    { Toolbar }
    procedure DrawToolbar(Canvas: TCanvas; R: TRect); virtual;

    { Spin Button }
    procedure DrawSpinButton(Canvas: TCanvas; R: TRect; State: TTeThemeButtonState; Up: boolean); virtual;

    { Grid }
    procedure DrawGridCell(Canvas: TCanvas; R: TRect; State: TGridDrawState); virtual;

    { Splitter }
    procedure DrawSplitter(Canvas: TCanvas; ARect: TRect; AHot, ABeveled: boolean); virtual;

    { Header }
    procedure DrawHeaderSection(Canvas: TCanvas; ARect: TRect; Section: TTeHeaderSection;
      AState: TTeSectionState); virtual;

    { Header }
    procedure DrawStatusBar(Canvas: TCanvas; ARect: TRect); virtual;
    procedure DrawStatusGripper(Canvas: TCanvas; ARect: TRect); virtual;
    procedure DrawStatusPanel(Canvas: TCanvas; ARect: TRect; Panel: TTeStatusPanel); virtual;

    { ScrollBox }
    procedure DrawScrollBox(Canvas: TCanvas; R: TRect; Enabled: boolean); virtual;

    { Region }
    function GetRegion(Width, Height: integer; BorderStyle: TTeBorderStyle): HRgn; virtual;

    { Return theme's name (need for selecting in IDE) }
    class function GetThemeName: string; virtual; 
    class function UseTheme: boolean; virtual; 
    { Properties }
    property DeltaHue: integer read FDeltaHue write SetDeltaHue;
    property DeltaBrightness: integer read FDeltaBrightness write SetDeltaBrightness;
    property BiDiMode: TBiDiMode read FBiDiMode write SetBiDiMode;
  published
  end;

  TTeThemeClass = class of TTeTheme;

implementation {===============================================================}

uses ksthemeengine;

{ TTeTheme }

constructor TTeTheme.Create;
begin
  inherited Create;
end;

destructor TTeTheme.Destroy;
begin
  inherited Destroy;
end;

function TTeTheme.DrawTextBiDiModeFlags(Flags: integer): integer;
begin
  Result := Flags;
  { do not change center alignment }
  if UseRightToLeftAlignment then
    if Result and DT_RIGHT = DT_RIGHT then
      Result := Result and not DT_RIGHT { removing DT_RIGHT, makes it DT_LEFT }
    else if not (Result and DT_CENTER = DT_CENTER) then
      Result := Result or DT_RIGHT;
  Result := Result or DrawTextBiDiModeFlagsReadingOnly;
end;

function TTeTheme.DrawTextBiDiModeFlagsReadingOnly: Longint;
begin
  if UseRightToLeftReading then
    Result := DT_RTLREADING
  else
    Result := 0;
end;

function TTeTheme.UseRightToLeftReading: Boolean;
begin
  Result := SysLocale.MiddleEast and (BiDiMode <> bdLeftToRight);
end;

function TTeTheme.UseRightToLeftAlignment: Boolean;
begin
  Result := SysLocale.MiddleEast and (BiDiMode = bdRightToLeft);
end;

function TTeTheme.UseRightToLeftScrollBar: Boolean;
begin
  Result := SysLocale.MiddleEast and
    (BiDiMode in [bdRightToLeft, bdRightToLeftNoAlign]);
end;

procedure TTeTheme.CalcMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem;
  var AWidth, AHeight: integer);
begin
end;

procedure TTeTheme.CalcMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
  var AWidth, AHeight: integer);
begin
end;

procedure TTeTheme.DrawButton(Canvas: TCanvas; Width, Height: integer;
  State: TTeThemeButtonState);
begin
end;

procedure TTeTheme.DrawCheckBox(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
begin
end;

procedure TTeTheme.DrawGroupBox(Canvas: TCanvas; R, CaptionRect: TRect;
  Caption: WideString);
begin

end;

procedure TTeTheme.DrawMenuBar(Canvas: TCanvas; Width, Height: integer);
begin

end;

procedure TTeTheme.DrawMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
begin

end;

procedure TTeTheme.DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
begin

end;

procedure TTeTheme.DrawMenuScrollButton(Canvas: TCanvas; Rect: TRect;
  Button: TTeMenuScrollButton; Active: boolean);
begin

end;

procedure TTeTheme.DrawPanel(Canvas: TCanvas; R: TRect;
  ShowBevel, ShowCaption: boolean);
begin

end;

procedure TTeTheme.DrawPanelButton(Canvas: TCanvas; R: TRect;
  Kind: TTePanelButtonKind; State: TTeThemeButtonState; Rolled: boolean);
begin

end;

procedure TTeTheme.DrawPanelCaption(Canvas: TCanvas; R: TRect;
  Caption: WideString);
begin

end;

procedure TTeTheme.DrawPopupMenu(Canvas: TCanvas; Width,
  Height: integer);
begin

end;

procedure TTeTheme.DrawProgessBar(Canvas: TCanvas; R, BarR: TRect;
  Orientation: TTeBarOrientation; Smooth: boolean);
begin

end;

procedure TTeTheme.DrawProgessFrame(Canvas: TCanvas; R: TRect);
begin

end;

procedure TTeTheme.DrawRadioButton(Canvas: TCanvas; Rect: TRect;
  ButtonState: TTeThemeButtonState; CheckState: TTeThemeCheckBoxState);
begin

end;

procedure TTeTheme.DrawScrollBar(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind);
begin

end;

procedure TTeTheme.DrawScrollBarButton(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; LeftTop: boolean; State: TTeThemeButtonState);
begin

end;

procedure TTeTheme.DrawScrollBarSlider(Canvas: TCanvas; R: TRect;
  Kind: TScrollBarKind; State: TTeThemeButtonState);
begin

end;

procedure TTeTheme.DrawSysButton(Canvas: TCanvas; R: TRect;
  Kind: TTeThemeSysButton; Hot, Down, Active: boolean;
  BorderStyle: TTeBorderStyle);
begin

end;

procedure TTeTheme.DrawTrackBar(Canvas: TCanvas; R, HighlightR: TRect);
begin

end;

procedure TTeTheme.DrawTrackBarThumb(Canvas: TCanvas; R: TRect;
  Orientation: TTrackOrientation; TickMarks: TTickMark;
  State: TTeThemeButtonState);
begin

end;

procedure TTeTheme.DrawWindow(Canvas: TCanvas; Width, Height: integer;
  ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle);
begin

end;

procedure TTeTheme.DrawMenuBarIcons(Canvas: TCanvas; Item: TTeCustomItem;
  Rect: TRect; Active, Hover: boolean);
begin

end;

procedure TTeTheme.DrawTabBorder(Canvas: TCanvas; R: TRect);
begin

end;

procedure TTeTheme.DrawTab(Canvas: TCanvas; R: TRect;  TabPosition: TTabPosition;
  State: TTeThemeButtonState);
begin

end;

procedure TTeTheme.DrawControlFrame(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState);
begin
end;

function TTeTheme.GetColor(Color: TTeThemeColor): TColor;
begin

end;

function TTeTheme.GetFontName(Font: TTeThemeFont): string;
begin

end;

function TTeTheme.GetFontSize(Font: TTeThemeFont): integer;
begin

end;

function TTeTheme.GetFontStyle(Font: TTeThemeFont): TFontStyles;
begin

end;

function TTeTheme.GetMetrix(Metrix: TTeThemeMetrix): integer;
begin

end;

{ Class methods }

class function TTeTheme.GetThemeName: string;
begin
  Result := 'No Theme';
end;

class function TTeTheme.UseTheme: boolean;
begin
  Result := false;
end;

{ Transform }

procedure TTeTheme.ChangeThemeColors;
begin
end;

{ Properties }

procedure TTeTheme.SetDeltaHue(const Value: integer);
begin
  if FDeltaHue <> Value then
  begin
    FDeltaHue := Value;
    ChangeThemeColors;
  end;
end;

procedure TTeTheme.SetDeltaBrightness(const Value: integer);
begin
  if FDeltaBrightness <> Value then
  begin
    FDeltaBrightness := Value;
    ChangeThemeColors;
  end;
end;

procedure TTeTheme.DrawComboButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState);
begin

end;

procedure TTeTheme.DrawSpeedButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
begin

end;

procedure TTeTheme.DrawSpeedButtonChevron(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Flat, Exclusive: boolean);
begin

end;

procedure TTeTheme.DrawSpinButton(Canvas: TCanvas; R: TRect;
  State: TTeThemeButtonState; Up: boolean);
begin

end;

function TTeTheme.GetRegion(Width, Height: integer;
  BorderStyle: TTeBorderStyle): HRgn;
begin
  Result := 0;
end;

procedure TTeTheme.DrawTabScrollButton(Canvas: TCanvas; R: TRect; LeftTop: boolean;
  TabPosition: TTabPosition; State: TTeThemeButtonState);
begin

end;

procedure TTeTheme.SetBiDiMode(const Value: TBiDiMode);
begin
  FBiDiMode := Value;
end;

procedure TTeTheme.DrawControlBar(Canvas: TCanvas; R: TRect);
begin

end;

procedure TTeTheme.DrawControlBarFrame(Canvas: TCanvas; R, GrabberRect: TRect);
begin

end;

procedure TTeTheme.DrawToolbar(Canvas: TCanvas; R: TRect);
begin

end;

procedure TTeTheme.DrawGridCell(Canvas: TCanvas; R: TRect;
  State: TGridDrawState);
begin

end;

function TTeTheme.HasCaptionTextShadow: boolean;
begin
  Result := true;
end;

procedure TTeTheme.DrawSplitter(Canvas: TCanvas; ARect: TRect;
  AHot, ABeveled: boolean);
begin

end;

procedure TTeTheme.DrawHeaderSection(Canvas: TCanvas; ARect: TRect;
  Section: TTeHeaderSection; AState: TTeSectionState);
begin

end;

procedure TTeTheme.DrawStatusBar(Canvas: TCanvas; ARect: TRect);
begin

end;

procedure TTeTheme.DrawStatusGripper(Canvas: TCanvas; ARect: TRect);
begin

end;

procedure TTeTheme.DrawStatusPanel(Canvas: TCanvas; ARect: TRect;
  Panel: TTeStatusPanel);
begin

end;

procedure TTeTheme.DrawScrollBox(Canvas: TCanvas; R: TRect;
  Enabled: boolean);
begin

end;

initialization
  RegisterTheme(TTeTheme);
finalization
end.




