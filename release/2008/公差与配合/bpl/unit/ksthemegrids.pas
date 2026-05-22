{==============================================================================

  ThemeEngine's Grids
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemegrids.pas,v 1.3 2002/10/28 21:04:00 Evgeny Exp $

===============================================================================}

unit ksthemegrids;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Grids,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeStringGrid class }

  TTeThemeStringGrid = class(TTeCustomStringGrid)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); override;
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure DrawCell(ACol, ARow: Longint; ARect: TRect; AState: TGridDrawState); override;
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

{ TTeThemeDrawGrid class }

  TTeThemeDrawGrid = class(TTeCustomDrawGrid)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); override;
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure DrawCell(ACol, ARow: Longint; ARect: TRect; AState: TGridDrawState); override;
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

uses ksthemescrollbars;

{ TTeThemeStringGrid ===============================================================}

constructor TTeThemeStringGrid.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeStringGrid.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeStringGrid.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

function TTeThemeStringGrid.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeStringGrid.DrawCell(ACol, ARow: Integer; ARect: TRect;
  AState: TGridDrawState);
begin
  if UseTheme then
  begin
    if goRowSelect in Options then
      if gdSelected in AState then
        AState := [gdFocused];

    FThemeEngine.Theme.DrawGridCell(Canvas, ARect, AState);

    inherited;
    Brush.Style := bsClear;
    Canvas.TextRect(ARect, ARect.Left+2, ARect.Top+2, Cells[ACol, ARow]);
  end
  else
    inherited;
end;

procedure TTeThemeStringGrid.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FThemeEngine.Theme.DrawScrollBox(Canvas, ARect, Enabled);
  end;
end;

{ Properties }

function TTeThemeStringGrid.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeStringGrid.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  DefaultDrawing := not ((FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme) and not (csDestroying in ComponentState));

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  Invalidate;
end;

procedure TTeThemeStringGrid.SetVersion(const Value: TTeThemeVersion);
begin

end;

{ TTeThemeDrawGrid ===============================================================}

constructor TTeThemeDrawGrid.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeDrawGrid.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeDrawGrid.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

function TTeThemeDrawGrid.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

{ Properties }

function TTeThemeDrawGrid.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeDrawGrid.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  DefaultDrawing := not ((FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme) and not (csDestroying in ComponentState));

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  Invalidate;
end;

procedure TTeThemeDrawGrid.SetVersion(const Value: TTeThemeVersion);
begin

end;

procedure TTeThemeDrawGrid.DrawCell(ACol, ARow: Integer; ARect: TRect;
  AState: TGridDrawState);
begin
  if UseTheme then
  begin
    if goRowSelect in Options then
      if gdSelected in AState then
        AState := [gdFocused];

    FThemeEngine.Theme.DrawGridCell(Canvas, ARect, AState);
    inherited;
  end
  else
    inherited;
end;

procedure TTeThemeDrawGrid.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FThemeEngine.Theme.DrawScrollBox(Canvas, ARect, Enabled);
  end;
end;

end.
