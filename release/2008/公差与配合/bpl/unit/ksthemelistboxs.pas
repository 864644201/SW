{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemelistboxs.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemelistboxs;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

  TTeThemeListBox = class(TTeCustomListBox)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    function CreateScrollBar: TTeCustomScrollBar; override;
    function GetChckBxFieldWidth: integer; override;
    function GetCheckBoxSize: TPoint; override;
    function GetItemHeight(Index: integer): integer; override;

    procedure PaintBorder; override;
    
    procedure DrawBackground; override;
    procedure DrawItem(Canvas: TCanvas; Index: integer; ARect: TRect); override;
    procedure DrawCheckBox(Index: integer; ARect: TRect); override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
  published
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

implementation {===============================================================}

uses KsThemeScrollBars;

{ TTeThemeListBox }

constructor TTeThemeListBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
  Color := KColorToColor(ckWindow);
end;

destructor TTeThemeListBox.Destroy;
begin
  inherited;
end;

function TTeThemeListBox.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeListBox.CreateScrollBar: TTeCustomScrollBar;
begin
  Result := TTeThemeScrollBar.Create(Self);
  (Result as TTeThemeScrollBar).ThemeEngine := FThemeEngine;
end;

function TTeThemeListBox.GetChckBxFieldWidth: integer;
begin
  Result := inherited GetChckBxFieldWidth;
  if UseTheme then
    Result := Result + 6;
end;

function TTeThemeListBox.GetCheckBoxSize: TPoint;
begin
  if UseTheme then
    Result := Point(FThemeEngine.Theme.GetMetrix(ngmCheckBoxWidth),
      FThemeEngine.Theme.GetMetrix(ngmCheckBoxHeight))
  else
    Result := inherited GetCheckBoxSize;
end;

function TTeThemeListBox.GetItemHeight(Index: integer): integer;
begin
  Result := inherited GetItemHeight(Index);
end;

{ Drawing }

procedure TTeThemeListBox.DrawCheckBox(Index: integer; ARect: TRect);
var
  State: TTeThemeButtonState;
  CheckState: TTeThemeCheckBoxState;
begin
  if not UseTheme then
  begin
    inherited;
    Exit;
  end;

  if not Enabled then
    State := ngsDisabled
  else
    if MouseOnItemIndex = Index then
      State := ngsHot
    else
      State := ngsNormal;

  CheckState := TTeThemeCheckBoxState(Self.State[Index]);

  FThemeEngine.Theme.DrawCheckBox(Canvas, ARect, State, CheckState);
end;

procedure TTeThemeListBox.DrawItem(Canvas: TCanvas; Index: integer; ARect: TRect);
begin
  inherited;
end;

procedure TTeThemeListBox.DrawBackground;
begin
  if UseTheme then
    FillRect(Canvas, GetBorderRect, FThemeEngine.Theme.GetColor(ngcWindow))
  else
    inherited;
end;

procedure TTeThemeListBox.PaintBorder;
var
  R: TRect;
  State: TTeThemeButtonState;
begin
  if not UseTheme then
  begin
    inherited;
    Exit;
  end;

  R := Rect(0, 0, FWidth, FHeight);

  if not Enabled then
    State := ngsDisabled
  else
    State := ngsNormal;

  FThemeEngine.Theme.DrawControlFrame(Canvas, Classes.Rect(0, 0, FWidth, FHeight), State);
end;

procedure TTeThemeListBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeListBox.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeListBox.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeListBox.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  if FScrlBar <> nil then
    (FScrlBar as TTeThemeScrollBar).ThemeEngine := FThemeEngine;
  Invalidate;
end;

end.

