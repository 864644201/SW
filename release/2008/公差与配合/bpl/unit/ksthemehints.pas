{==============================================================================

  ThemeEngine's Button
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemehints.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemehints;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, StdCtrls, te_controls, KsThemeThemes, KsThemeEngine,
  KsThemeVersion;

type

{ TTeThemeHint }

  TTeThemeHint = class(TTeCustomHint)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    function UseTheme: boolean;
  protected
    function GetHintBounds(Canvas: TCanvas; const AHint: string): TRect; override;
    procedure PaintHint(Canvas: TCanvas; R: TRect; const AHint: string); override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    { Public declarations }
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

{ TTeThemeHint ===============================================================}

constructor TTeThemeHint.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeHint.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeHint.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Overrides }

function TTeThemeHint.GetHintBounds(Canvas: TCanvas; const AHint: string): TRect;
begin
  Result := Rect(0, 0, 1000, 1000);
  if (Owner is TWinControl) then
  DrawText(Canvas, AHint, Result, (Owner as TWinControl).DrawTextBiDiModeFlags(DT_CALCRECT or DT_LEFT or DT_WORDBREAK or DT_NOPREFIX));

  Inc(Result.Right, 6);
  Inc(Result.Bottom, 6);
  if Shadow.Enabled then
  begin
    Inc(Result.Right, Shadow.Size);
    Inc(Result.Bottom, Shadow.Size);
  end;
end;

procedure TTeThemeHint.PaintHint(Canvas: TCanvas; R: TRect; const AHint: string);
begin
  if UseTheme then
  begin
    FillRect(Canvas, R, FThemeEngine.Theme.GetColor(ngcWindow));
    DrawRect(Canvas, R, FThemeEngine.Theme.GetColor(ngcBorder));

    if Owner is TWinControl then
      DrawText(Canvas, AHint, R,
        (Owner as TWinControl).DrawTextBiDiModeFlags(DT_Center or DT_SINGLELINE or DT_VCenter));
  end
  else
    inherited ;
end;

procedure TTeThemeHint.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemeHint.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeHint.SetVersion(const Value: TTeThemeVersion);
begin
end;

procedure TTeThemeHint.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
end;

end.
