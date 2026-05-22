{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemespinbuttons.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemespinbuttons;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, Menus, Buttons, te_controls, KsThemeSpeedButtons, KsThemeThemes,
  KsThemeEngine, KsThemeVersion;

type

{ TTeThemeSpinButton }

  TTeThemeSpinButton = class (TTeCustomSpinButton)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    function CreateButton: TTeCustomSpeedButton; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
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

{ TTeThemeSpinButton }

constructor TTeThemeSpinButton.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

function TTeThemeSpinButton.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeSpinButton.CreateButton: TTeCustomSpeedButton;
begin
  Result := TTeThemeSpeedButton.Create(Self);
  Result.Parent := Self;
  Result.Flat := true;
end;

procedure TTeThemeSpinButton.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeSpinButton.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeSpinButton.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  if UpButton <> nil then
  begin
    (UpButton as TTeThemeSpeedButton).ThemeEngine := Value;
    (DownButton as TTeThemeSpeedButton).ThemeEngine := Value;
  end;
end;

procedure TTeThemeSpinButton.SetVersion(const Value: TTeThemeVersion);
begin
end;

end.
