{==============================================================================

  ThemeEngine's ControlBar
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemecontrolbars.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemecontrolbars;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, SysUtils, Messages, Classes, Graphics, Controls, Forms,
  Dialogs, Menus, StdCtrls, ExtCtrls, Buttons, ActnList, Consts, te_controls,
  KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

  TTeThemeControlBar = class(TTeCustomControlBar)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    procedure PaintControlFrame(Canvas: TCanvas; AControl: TControl;
      var ARect: TRect); override;
    procedure PaintBackground; override;
    function GetGrabberSize: integer; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
  published
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Transparent;
    property Version: TTeThemeVersion read GetVersion write SetVersion
      stored false;
  end;

implementation {===============================================================}

{ TTeThemeControlBar }

constructor TTeThemeControlBar.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeControlBar.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeControlBar.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeControlBar.GetGrabberSize: integer;
begin
  if (csDestroying in ComponentState) then
    Result := inherited GetGrabberSize
  else
    if (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and (FThemeEngine.Theme.UseTheme)
    then
      Result := FThemeEngine.Theme.GetMetrix(ngmGrabberSize)
    else
      Result := inherited GetGrabberSize;
end;

procedure TTeThemeControlBar.PaintControlFrame(Canvas: TCanvas; AControl: TControl;
  var ARect: TRect);
var
  GrabberRect: TRect;
begin
  if not UseTheme then
  begin
    inherited ;
  end
  else
  begin
    GrabberRect := ARect;
    InflateRect(GrabberRect, 0, -2);
    GrabberRect.Right := GrabberRect.Left + GetGrabberSize;
    
    FThemeEngine.Theme.DrawControlBarFrame(Canvas, ARect, GrabberRect);
  end;
end;

procedure TTeThemeControlBar.PaintBackground;
begin
  if not UseTheme then
  begin
    inherited ;
  end
  else
  begin
    FThemeEngine.Theme.DrawControlBar(Canvas, Rect(0, 0, Width, Height));
  end;
end;

{ Properties }

function TTeThemeControlBar.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeControlBar.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  ResetDockItems;
  Invalidate;
end;

procedure TTeThemeControlBar.SetVersion(const Value: TTeThemeVersion);
begin
end;

end.

