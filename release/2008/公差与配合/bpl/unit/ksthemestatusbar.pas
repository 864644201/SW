{==============================================================================

  ThemeEngine's StatusBar
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemestatusbar.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit ksthemestatusbar;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeStatusBar class }

  TTeThemeStatusBar = class(TTeCustomStatusBar)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure DrawPanel(Panel: TTeStatusPanel; ARect: TRect; AGripper: boolean); override;
    procedure PaintBackGround; override;
    procedure PaintGripper; override;
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

{ TTeThemeStatusBar ===============================================================}

constructor TTeThemeStatusBar.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeStatusBar.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeStatusBar.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeStatusBar.PaintBackGround;
var
  SaveIndex: integer;
begin
  if not UseTheme then
    inherited
  else
  begin
    SaveIndex := SaveDC(Canvas.Handle);

    if Panels.Count > 0 then
      with GetPanelsRect do
        ExcludeClipRect(Canvas.Handle, Left, Top, Right, Bottom);

    FThemeEngine.Theme.DrawStatusBar(Canvas, Rect(0, 0, Width, Height));

    RestoreDC(Canvas.Handle, SaveIndex);
  end;
end;

procedure TTeThemeStatusBar.PaintGripper;
begin
  if not UseTheme then
    inherited
  else
  begin
    FThemeEngine.Theme.DrawStatusGripper(Canvas, GetGripperRect);
  end;
end;

procedure TTeThemeStatusBar.DrawPanel(Panel: TTeStatusPanel; ARect: TRect; AGripper: boolean);
begin
  if UseTheme then
  begin
    FThemeEngine.Theme.DrawStatusPanel(Canvas, ARect, Panel);

    if Panel.Style = psOwnerDraw then
      DoDrawPanel(Self, Panel, ARect)
    else
    begin
      DrawPanelText(Panel, ARect);
      if Panel.ShowImage then
        DrawPanelImage(Panel, ARect);
    end;
  end
  else
    inherited;
end;

{ Properties }

function TTeThemeStatusBar.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeStatusBar.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

procedure TTeThemeStatusBar.SetVersion(const Value: TTeThemeVersion);
begin

end;

end.
