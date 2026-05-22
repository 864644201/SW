{==============================================================================

  ThemeEngine's Standard Control
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemestdcontrol.pas,v 1.4 2002/08/14 11:30:19 Evgeny Exp $

===============================================================================}

unit ksthemestdcontrol;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeMaskEdit class }

  TTeThemeMaskEdit = class(TTeCustomMaskEdit)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure PaintBuffer(Canvas: TCanvas; ARect: TRect); override;
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

{ TTeThemeScrollBox class }

  TTeThemeScrollBox = class(TTeCustomScrollBox)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure PaintBuffer(Canvas: TCanvas; ARect: TRect); override;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); override;
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

{ TTeThemeMemo class }

  TTeThemeMemo = class(TTeCustomMemo)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure PaintBuffer(Canvas: TCanvas; ARect: TRect); override;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); override;
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

{ TTeThemeSTreeView class }

  TTeThemeSTreeView = class(TTeCustomTreeView)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure PaintBuffer(Canvas: TCanvas; ARect: TRect); override;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); override;
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

{ TTeThemeSListView class }

  TTeThemeSListView = class(TTeCustomListView)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure PaintBorder(Canvas: TCanvas; ARect: TRect); override;
    procedure PaintBuffer(Canvas: TCanvas; ARect: TRect); override;
    procedure CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent); override;
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

uses KsThemeScrollbars;

{ TTeThemeMaskEdit ===============================================================}

constructor TTeThemeMaskEdit.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeMaskEdit.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeMaskEdit.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeMaskEdit.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    if BorderStyle = bsSingle then
      if Enabled then
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsNormal)
      else
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsDisabled)
  end;
end;

procedure TTeThemeMaskEdit.PaintBuffer(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FillRect(Canvas, ARect, FThemeEngine.Theme.GetColor(ngcWindow));
  end;
end;

{ Properties }

function TTeThemeMaskEdit.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeMaskEdit.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  if UseTheme then
    Color := FThemeEngine.Theme.GetColor(ngcWindow)
  else
    Color := clWindow;
  Invalidate;
end;

procedure TTeThemeMaskEdit.SetVersion(const Value: TTeThemeVersion);
begin

end;


{ TTeThemeScrollBox ===============================================================}


constructor TTeThemeScrollBox.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeScrollBox.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeScrollBox.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeScrollBox.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

procedure TTeThemeScrollBox.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    if BorderStyle = bsSingle then
      FThemeEngine.Theme.DrawScrollBox(Canvas, ARect, Enabled);
  end;
end;

procedure TTeThemeScrollBox.PaintBuffer(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FillRect(Canvas, ARect, FThemeEngine.Theme.GetColor(ngcBtnFace));
  end;
end;

{ Properties }

function TTeThemeScrollBox.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeScrollBox.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  Invalidate;
end;

procedure TTeThemeScrollBox.SetVersion(const Value: TTeThemeVersion);
begin

end;


{ TTeThemeMemo ===============================================================}


constructor TTeThemeMemo.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeMemo.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeMemo.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeMemo.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

procedure TTeThemeMemo.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    if BorderStyle = bsSingle then
      if Enabled then
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsNormal)
      else
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsDisabled)
  end;
end;

procedure TTeThemeMemo.PaintBuffer(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FillRect(Canvas, ARect, FThemeEngine.Theme.GetColor(ngcWindow));
  end;
end;

{ Properties }

function TTeThemeMemo.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeMemo.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  if UseTheme then
    Color := FThemeEngine.Theme.GetColor(ngcWindow)
  else
    Color := clWindow;
    
  Invalidate;
end;

procedure TTeThemeMemo.SetVersion(const Value: TTeThemeVersion);
begin

end;

{ TTeThemeSTreeView ===============================================================}


constructor TTeThemeSTreeView.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeSTreeView.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeSTreeView.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeSTreeView.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

procedure TTeThemeSTreeView.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if UseTheme then
    Color := FThemeEngine.Theme.GetColor(ngcWindow)
  else
    Color := clWindow;
    
  if not UseTheme then
    inherited
  else
  begin
    if BorderStyle = bsSingle then
      if Enabled then
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsNormal)
      else
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsDisabled);
  end;
end;

procedure TTeThemeSTreeView.PaintBuffer(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FillRect(Canvas, ARect, FThemeEngine.Theme.GetColor(ngcWindow));
  end;
end;

{ Properties }

function TTeThemeSTreeView.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeSTreeView.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  Invalidate;
end;

procedure TTeThemeSTreeView.SetVersion(const Value: TTeThemeVersion);
begin

end;


{ TTeThemeSListView ===============================================================}


constructor TTeThemeSListView.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeSListView.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeSListView.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeSListView.CreateScrollBar(var AScrollBar: TTeCustomScrollBar; AOwner: TComponent);
begin
  AScrollBar := TTeThemeScrollBar.Create(AOwner);
end;

procedure TTeThemeSListView.PaintBorder(Canvas: TCanvas; ARect: TRect);
begin
  if UseTheme then
    Color := FThemeEngine.Theme.GetColor(ngcWindow)
  else
    Color := clWindow;

  if not UseTheme then
    inherited
  else
  begin
    if BorderStyle = bsSingle then
      if Enabled then
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsNormal)
      else
        FThemeEngine.Theme.DrawControlFrame(Canvas, ARect, ngsDisabled);
  end;
end;

procedure TTeThemeSListView.PaintBuffer(Canvas: TCanvas; ARect: TRect);
begin
  if not UseTheme then
    inherited
  else
  begin
    FillRect(Canvas, ARect, FThemeEngine.Theme.GetColor(ngcWindow));
  end;
end;

{ Properties }

function TTeThemeSListView.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeSListView.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;

  if SubScrollBars <> nil then
  begin
    TTeThemeScrollBar(SubScrollBars.HScrollBar.ScrollBar).ThemeEngine := Value;
    TTeThemeScrollBar(SubScrollBars.VScrollBar.ScrollBar).ThemeEngine := Value;
  end;

  if UseTheme then
    Color := FThemeEngine.Theme.GetColor(ngcWindow)
  else
    Color := clWindow;

  Invalidate;
end;

procedure TTeThemeSListView.SetVersion(const Value: TTeThemeVersion);
begin

end;


end.
