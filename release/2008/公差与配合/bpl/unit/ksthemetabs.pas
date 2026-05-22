{==============================================================================

  ThemeEngine's TabControl
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemetabs.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemetabs;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, te_controls, KsThemeVersion, KsThemeThemes, KsThemeEngine;

type

{ TTeThemeTabControl }

  TTeThemeTabControl = class(TTeCustomTabControl)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { inherited }
    function GetLeftMargin: integer; override;
    function GetTopMargin: integer; override;
    function GetRightMargin: integer; override;
    function GetBottomMargin: integer; override;
    function GetTabHeight: integer; override;

    procedure DrawTab(ATabIndex: integer); override;
    procedure DrawBorder; override;
    procedure DrawLeftScrlBtn; override;
    procedure DrawRightScrlBtn; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

{ TTeThemePageControl }

  TTeThemePageControl = class(TTeThemeTabControl)
  private
    FUsePages: boolean;
  public
    constructor Create(AOwner: TComponent); override;
  published
    property UsePages: boolean read FUsePages;
  end;

implementation {===============================================================}

{ TTeThemeTabControl }

constructor TTeThemeTabControl.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  Transparent := true;
end;

destructor TTeThemeTabControl.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeTabControl.Loaded;
begin
  inherited Loaded;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemeTabControl.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ inherited }

function TTeThemeTabControl.GetBottomMargin: integer;
begin
  if not UseTheme then
    Result := inherited GetBottomMargin
  else
    Result := FThemeEngine.Theme.GetMetrix(ngmTabMargin);
end;

function TTeThemeTabControl.GetLeftMargin: integer;
begin
  if not UseTheme then
    Result := inherited GetLeftMargin
  else
    Result := FThemeEngine.Theme.GetMetrix(ngmTabMargin);
end;

function TTeThemeTabControl.GetRightMargin: integer;
begin
  if not UseTheme then
    Result := inherited GetRightMargin
  else
    Result := FThemeEngine.Theme.GetMetrix(ngmTabMargin);
end;

function TTeThemeTabControl.GetTopMargin: integer;
begin
  if not UseTheme then
    Result := inherited GetTopMargin
  else
    Result := FThemeEngine.Theme.GetMetrix(ngmTabMargin);
end;

function TTeThemeTabControl.GetTabHeight: integer;
begin
  if not UseTheme then
    Result := inherited GetTabHeight
  else
    Result := FThemeEngine.Theme.GetMetrix(ngmTabHeight);
end;

{ Drawing }

procedure TTeThemeTabControl.DrawBorder;
var
  R: TRect;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  R := GetBorderRect;
  FThemeEngine.Theme.DrawTabBorder(Canvas, R);
end;

procedure TTeThemeTabControl.DrawTab(ATabIndex: integer);
var
  R: TRect;
  State: TTeThemeButtonState;
  Index: integer;
begin
  if not UseTheme then
  begin
    inherited ;
    Exit;
  end;

  R := GetTabRect(ATabIndex);

  if ATabIndex = TabIndex then
    State := ngsFocused
  else
    if MouseOnTabIndex = ATabIndex then
      State := ngsHot
    else
      State := ngsNormal;

  { Draw tab }
  if TabIndex <> ATabIndex then
    if TabPosition in [tpLeft, tpRight] then
      InflateRect(R, -GetLeftMargin, 0)
    else
      InflateRect(R, 0, -GetLeftMargin);

  if (ATabIndex = 0) and (TabPosition in [tpTop, tpBottom]) then
    R.Left := 0;

  FThemeEngine.Theme.DrawTab(Canvas, R, TabPosition, State);

  if TabPosition in [tpTop, tpBottom] then
    if RectWidth(R) < 8 then Exit;

  { Draw Glyph }
  if UsePages and (Images <> nil) then
  begin
    Index := GetPageIndexFromTabIndex(ATabIndex);
    if (Index >= 0) and (Index < Images.Count) then
    begin
      if Pages[Index].ImageIndex >= 0 then
      begin
        DrawTabGlyph(ATabIndex, TabPosition, R);
        { Change LRect for DrawText }
        case TabPosition of
          tpLeft, tpBottom: Dec(R.Bottom, GetTabHeight);
          tpTop: Inc(R.Left, GetTabHeight);
          tpRight: Inc(R.Top, GetTabHeight);
        end;
      end;
    end;
  end;

  { Draw Text }
  case TabPosition of
    tpLeft: begin
      R.Right := R.Left + GetTabHeight;
      DrawVerticalText(Canvas, Tabs[ATabIndex], R, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE), false)
    end;
     tpTop: begin
      R.Bottom := R.Top + GetTabHeight;
      DrawText(Canvas, Tabs[ATabIndex], R, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE))
    end;
    tpRight: begin
      R.Left := R.Right - GetTabHeight;
      DrawVerticalText(Canvas, Tabs[ATabIndex], R, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE), true)
    end;
    tpBottom: begin
      R.Top := R.Bottom - GetTabHeight;
      DrawText(Canvas, Tabs[ATabIndex], R, DrawTextBiDiModeFlags(DT_Center or DT_VCenter or DT_SINGLELINE))
    end;
  end;
end;

procedure TTeThemeTabControl.DrawLeftScrlBtn;
var
  R: TRect;
  State: TTeThemeButtonState;
begin
  if not GetScrlBtnsVisible then Exit;

  if UseTheme then
  begin
    R := GetLeftScrlBtnRect;

    if LeftBtnPressed then
      State := ngsPressed
    else
      if MouseOnLeftBtn then
        State := ngsHot
      else
        State := ngsNormal;

    FThemeEngine.Theme.DrawTabScrollButton(Canvas, R, true, TabPosition, State);
  end
  else
    inherited;
end;

procedure TTeThemeTabControl.DrawRightScrlBtn;
var
  R: TRect;
  State: TTeThemeButtonState;
begin
  if not GetScrlBtnsVisible then Exit;

  if UseTheme then
  begin
    R := GetRightScrlBtnRect;

    if RightBtnPressed then
      State := ngsPressed
    else
      if MouseOnRightBtn then
        State := ngsHot
      else
        State := ngsNormal;

    FThemeEngine.Theme.DrawTabScrollButton(Canvas, R, false, TabPosition, State);
  end
  else
    inherited;
end;

{ VCL protected }

procedure TTeThemeTabControl.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeTabControl.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeTabControl.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeTabControl.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

{ TTeThemePageControl }

constructor TTeThemePageControl.Create(AOwner: TComponent);
begin
  inherited;

  inherited UsePages := true;
end;

end.

