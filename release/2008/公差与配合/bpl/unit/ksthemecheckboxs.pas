{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemecheckboxs.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemecheckboxs;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Dialogs,
  StdCtrls, Forms, te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeCheckBox class }

{ A TTeThemeCheckBox component presents an option for the user. The user can check the box to select the option, or uncheck it to deselect the option. }
  TTeThemeCheckBox = class(TTeCustomCheckBox)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { overrides }
    function GetBoxSize: TPoint; override;
    procedure PaintBox; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure Loaded; override;
  published
    property Caption;
    property Checked;
    property Enabled;
    property Font;
    property Spacing;
    property State;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

{ TTeThemeRadioButton }

{ Use TTeThemeRadioButton to add a radio button to a form. Radio buttons present a set of mutually exclusive options to the user-that is, only one radio button in a set can be selecTeThemed at a time. }  
  TTeThemeRadioButton = class(TTeCustomRadioButton)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { overrides }
    function GetBoxSize: TPoint; override;
    procedure PaintBox; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    procedure Loaded; override;
  published
    property Caption;
    property Checked;
    property Enabled;
    property Font;
    property GroupIndex;
    property Spacing;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

implementation {===============================================================}

{ TTeThemeCheckBox }

constructor TTeThemeCheckBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

procedure TTeThemeCheckBox.Loaded;
begin
  inherited;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemeCheckBox.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeCheckBox.GetBoxSize: TPoint;
begin
  if UseTheme then
    Result := Point(FThemeEngine.Theme.GetMetrix(ngmCheckBoxWidth), FThemeEngine.Theme.GetMetrix(ngmCheckBoxHeight))
  else
    Result := inherited GetBoxSize;
end;

procedure TTeThemeCheckBox.PaintBox;
var
  R: TRect;
  ButtonState: TTeThemeButtonState;
  CheckState: TTeThemeCheckBoxState;
begin
  if UseTheme then
  begin
    R := Rect(0, 0, GetBoxSize.X, GetBoxSize.Y);
    if Alignment = ktaLeftJustify then
      RectVCenter(R, Rect(0, 0, FWidth, FHeight))
    else
      RectVCenter(R, Rect(FWidth-GetBoxSize.X, 0, FWidth, FHeight));

    if not Enabled then
      ButtonState := ngsDisabled
    else
      if Pressed then
        ButtonState := ngsPressed
      else
        if MouseInControl then
          ButtonState := ngsHot
        else
          ButtonState := ngsNormal;

    case State of
      cbChecked: CheckState := ngcChecked;
      cbUnchecked: CheckState := ngcUnchecked;
      cbGrayed: CheckState := ngcMixed;
    end;

    FThemeEngine.Theme.DrawCheckBox(Canvas, R, ButtonState, CheckState);
  end
  else
    inherited;
end;

procedure TTeThemeCheckBox.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

procedure TTeThemeCheckBox.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

function TTeThemeCheckBox.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeCheckBox.SetVersion(const Value: TTeThemeVersion);
begin
end;

{ TTeThemeRadioButton =========================================================}

constructor TTeThemeRadioButton.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

procedure TTeThemeRadioButton.Loaded;
begin
  inherited;
{  ThemeEngine := FThemeEngine; }
end;

function TTeThemeRadioButton.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeRadioButton.GetBoxSize: TPoint;
begin
  if UseTheme then
    Result := Point(FThemeEngine.Theme.GetMetrix(ngmRadioButtonWidth), FThemeEngine.Theme.GetMetrix(ngmRadioButtonHeight))
  else
    Result := inherited GetBoxSize;
end;

procedure TTeThemeRadioButton.PaintBox;
var
  R: TRect;
  ButtonState: TTeThemeButtonState;
  CheckState: TTeThemeCheckBoxState;
begin
  if UseTheme then
  begin
    R := Rect(0, 0, GetBoxSize.X, GetBoxSize.Y);
    RectVCenter(R, Rect(0, 0, FWidth, FHeight));

    if not Enabled then
      ButtonState := ngsDisabled
    else
      if Pressed then
        ButtonState := ngsPressed
      else
        if MouseInControl then
          ButtonState := ngsHot
        else
          ButtonState := ngsNormal;

    if Checked then
      CheckState := ngcChecked
    else
      CheckState := ngcUnchecked;

    FThemeEngine.Theme.DrawRadioButton(Canvas, R, ButtonState, CheckState);
  end
  else
    inherited;
end;

procedure TTeThemeRadioButton.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeRadioButton.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeRadioButton.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeRadioButton.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

end.
