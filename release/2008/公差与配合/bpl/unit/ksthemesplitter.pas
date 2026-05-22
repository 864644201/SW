{==============================================================================

  ThemeEngine's Splitter
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemesplitter.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemesplitter;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeSplitter class }

  TTeThemeSplitter = class(TTeCustomSplitter)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBuffer; override;
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

{ TTeThemeSplitter ===============================================================}

constructor TTeThemeSplitter.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeSplitter.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeSplitter.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeSplitter.PaintBuffer;
begin
  if not UseTheme then
    inherited
  else
  begin
    FThemeEngine.Theme.DrawSplitter(Canvas, Rect(0, 0, Width, Height), MouseInControl, Beveled); 

    if csDesigning in ComponentState then
    with Canvas do
    begin
      Pen.Style := psDot;
      Pen.Mode := pmXor;
      Pen.Color := clBtnFace;
      Brush.Style := bsClear;
      Rectangle(0, 0, Width, Height);
    end; 
  end;
end;

{ Properties }

function TTeThemeSplitter.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeSplitter.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

procedure TTeThemeSplitter.SetVersion(const Value: TTeThemeVersion);
begin

end;

end.
