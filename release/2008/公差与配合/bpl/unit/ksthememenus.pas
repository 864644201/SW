{==============================================================================

  ThemeEngine's MenuBar and PopupMenu
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthememenus.pas,v 1.4 2002/10/28 21:04:00 Evgeny Exp $

===============================================================================}

unit ksthememenus;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, ExtCtrls,
  Menus, ImgList, te_controls, KsThemeItems, KsThemeThemes, KsThemeEngine,
  KsThemeVersion;

type

{ TTeThemeMenuBar class }

{ TTeThemeMenuBar is a menu bar and its accompanying drop-down menus for a form. }
  TTeThemeMenuBar = class(TTeCustomMenuBar)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { need for }
    class procedure GetItemClassProc(var AItemClass: TTeCustomItemClass); override;
    { for next }
    function GetViewRect: TRect; override;
    { override }
    procedure ItemsChanged; override;
    procedure PaintBuffer; override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property Align;
    property Anchors;
    property Images;
    property Items;
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion stored False;
  end;

{ TTeThemePopupMenu class }

{ TTeThemePopupMenu encapsulaTeThemes the properties, methods, and events of a advanced pop-up menu. }
  TTeThemePopupMenu = class(TTeCustomPopupMenu)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function UseTheme: boolean;
    { need for }
    class procedure GetItemClassProc(var AItemClass: TTeCustomItemClass); override;
    { VCL protected  }
    procedure Notification(AComponent: TComponent; Operation: TOperation); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;
  published
    property Items;
    property Images;
    { Specifies the appearance and behavior by using specified theme's from TTeThemeEngine .
      See Also:
        TTeThemeEngine
    }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
    property Version: TTeThemeVersion read GetVersion write SetVersion stored False;
  end;

implementation {===============================================================}

{ TTeThemeMenuBar ===============================================================}

constructor TTeThemeMenuBar.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeMenuBar.Destroy;
begin
  inherited Destroy;
end;

procedure TTeThemeMenuBar.Loaded;
begin
  inherited;
  ThemeEngine := FThemeEngine;
end;

class procedure TTeThemeMenuBar.GetItemClassProc(var AItemClass: TTeCustomItemClass);
begin
  AItemClass := TTeThemeItem;
end;

procedure TTeThemeMenuBar.ItemsChanged;
begin
  (Items as TTeThemeItem).ThemeEngine := FThemeEngine;
  inherited;
end;

{ Theme }

function TTeThemeMenuBar.UseTheme: boolean;
begin
  Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
    (FThemeEngine.Theme.UseTheme);
end;

function TTeThemeMenuBar.GetViewRect: TRect;
begin
  if UseTheme then
    Result := Rect(0, 0, FWidth, FHeight)
  else
    Result := inherited GetViewRect;
end;

procedure TTeThemeMenuBar.PaintBuffer;
begin
  if UseTheme then
  begin
    if Parent is TTeCustomControlBar then
      FThemeEngine.Theme.DrawToolBar(Canvas, Rect(0, 0, Width, Height))
    else
      FThemeEngine.Theme.DrawMenuBar(Canvas, Width, Height);

    if (View <> nil) then
    begin
      with GetViewRect do
      begin
        View.Left := Left;
        View.Top := Top;
      end;
      View.Paint(Canvas);
    end;
  end
  else
    inherited ;
end;

{ Properties }

function TTeThemeMenuBar.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeMenuBar.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  TTeThemeItem(Items).ThemeEngine := Value;
  Invalidate;
end;

procedure TTeThemeMenuBar.SetVersion(const Value: TTeThemeVersion);
begin
end;

{ TTeThemePopupMenu ==========================================================}

constructor TTeThemePopupMenu.Create(AOwner: TComponent);
begin
  inherited;
end;

destructor TTeThemePopupMenu.Destroy;
begin
  inherited;
end;

procedure TTeThemePopupMenu.Loaded;
begin
  inherited;
  ThemeEngine := FThemeEngine;
end;

class procedure TTeThemePopupMenu.GetItemClassProc(var AItemClass: TTeCustomItemClass);
begin
  AItemClass := TTeThemeItem;
end;

procedure TTeThemeMenuBar.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Theme }

function TTeThemePopupMenu.UseTheme: boolean;
begin
  Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
    (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemePopupMenu.Notification(AComponent: TComponent;
  Operation: TOperation);
begin
  inherited;
  if (Operation = opRemove) and (AComponent = FThemeEngine) then
    ThemeEngine := nil;
end;

{ Properties }

function TTeThemePopupMenu.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemePopupMenu.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  TTeThemeItem(Items).ThemeEngine := Value;
end;

procedure TTeThemePopupMenu.SetVersion(const Value: TTeThemeVersion);
begin
end;

end.
