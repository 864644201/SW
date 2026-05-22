{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthememessages.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthememessages;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, StdCtrls, Buttons, te_controls, KsThemeThemes,
  KsThemeEngine, KsThemeVersion;

type

{ TTeThemeMessage }

  TTeThemeMessage = class(TTeCustomMessage)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function CreateButton(Owner: TComponent): TTeCustomButton; override;
    function CreateEdit(Owner: TComponent): TTeCustomEdit; override;
    function CreateForm(Owner: TComponent): TTeCustomForm; override;
    function CreateLabel(Owner: TComponent): TTeCustomLabel; override;
    function CreateCheckBox(Owner: TComponent): TTeCustomCheckBox; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    { Public declarations }
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

uses KsThemeButtons, KsThemeForms, KsThemeEdits, KsThemeLabels, KsThemeCheckBoxs;

{ TTeThemeMessage }

constructor TTeThemeMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

function TTeThemeMessage.CreateButton(Owner: TComponent): TTeCustomButton;
begin
  Result := TTeThemeButton.Create(Owner);
  if Owner is TWinControl then
    Result.Parent := Owner as TWinControl;
  (Result as TTeThemeButton).ThemeEngine := FThemeEngine;
end;

function TTeThemeMessage.CreateEdit(Owner: TComponent): TTeCustomEdit;
begin
  Result := TTeThemeEdit.Create(Owner);
  if Owner is TWinControl then
    Result.Parent := Owner as TWinControl;
  (Result as TTeThemeEdit).ThemeEngine := FThemeEngine;
end;

function TTeThemeMessage.CreateForm(Owner: TComponent): TTeCustomForm;
begin
  Result := TTeThemeForm.Create(Owner);
  (Result as TTeThemeForm).ThemeEngine := FThemeEngine;
end;

function TTeThemeMessage.CreateCheckBox(Owner: TComponent): TTeCustomCheckBox;
begin
  Result := TTeThemeCheckBox.Create(Owner);
  if Owner is TWinControl then
    Result.Parent := Owner as TWinControl;
  (Result as TTeThemeCheckBox).ThemeEngine := FThemeEngine;
end;

function TTeThemeMessage.CreateLabel(Owner: TComponent): TTeCustomLabel;
begin
  Result := TTeThemeLabel.Create(Owner);
  if Owner is TWinControl then
    Result.Parent := Owner as TWinControl;
  (Result as TTeThemeLabel).ThemeEngine := FThemeEngine;
end;

procedure TTeThemeMessage.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

function TTeThemeMessage.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeMessage.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
end;

procedure TTeThemeMessage.SetVersion(const Value: TTeThemeVersion);
begin
end;

end.


