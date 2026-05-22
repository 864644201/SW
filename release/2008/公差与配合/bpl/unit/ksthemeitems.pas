{==============================================================================

  ThemeEngine Items
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeitems.pas,v 1.5 2002/10/28 21:04:00 Evgeny Exp $

===============================================================================}

unit ksthemeitems;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, CommCtrl, Menus, ActnList, ImgList, te_controls, ksthemethemes,
  ksthemeengine;

type

{ TTeThemeItem }

  TTeThemeItem = class(TTeCustomItem)
  private
    FThemeEngine: TTeThemeEngine;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
  protected
    function CreatePopupWindowClass: TClass; override;
    { InTeThemernal }
    function UseTheme: boolean;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;

    procedure CalcSize(Canvas: TCanvas; View: TTeItemView; var AWidth,
      AHeight: integer); override;
    procedure DrawItem(Canvas: TCanvas; View: TTeItemView; Rect: TRect;
      Active, Hover: boolean); override;
    procedure DrawScrollButton(Canvas: TCanvas; View: TTeItemView; Rect: TRect;
      Button: TTeMenuScrollButton; Active: boolean); override;

    property ThemeEngine: TTeThemeEngine read FThemeEngine write SetThemeEngine;
  published
    property Checked;
    property Enabled;
    property Caption;
    property ImageIndex;
    property Images;
    property ShortCut;
    property Visible;
    property OnClick;
    property OnBeforeDropDown;
  end;

implementation {===============================================================}

uses ksthememenuform;

{ TTeThemeItem }

constructor TTeThemeItem.Create(AOwner: TComponent);
begin
  inherited;
end;

destructor TTeThemeItem.Destroy;
begin
  inherited;
end;

function TTeThemeItem.CreatePopupWindowClass: TClass;
begin
  Result := TTeThemePopupWindow;
end;

{ Theme }

function TTeThemeItem.UseTheme: boolean;
begin
  Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
    (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeItem.CalcSize(Canvas: TCanvas; View: TTeItemView;
  var AWidth, AHeight: integer);
begin
  if UseTheme and (Canvas <> nil) then
  begin
    if View.IsMenuBar then
      FThemeEngine.Theme.CalcMenuBarItem(Canvas, Self, AWidth, AHeight)
    else
    begin
      FThemeEngine.Theme.CalcMenuItem(Canvas, Self, AWidth, AHeight);
      if (Caption <> '-') and (AHeight < GetGlyphSize - 4) then
        AHeight := GetGlyphSize + 4;
    end;

    if MDIItemKind <> mikNone then
      AWidth := AHeight;
  end
  else
    inherited;
end;

procedure TTeThemeItem.DrawItem(Canvas: TCanvas; View: TTeItemView;
  Rect: TRect; Active, Hover: boolean);
var
  R: TRect;
  Flags: integer;
  IHandle, IHandle2 : HICON;
  IconX, IconY : integer;
  ID: boolean;
begin
  if UseTheme then
  begin
    { MDI Items }
    if MDIItemKind <> mikNone then
    begin
      if ActiveMDIForm = nil then Exit;
      
      FThemeEngine.Theme.DrawMenuBarIcons(Canvas, Self, Rect, Active, Hover);

      { Draw SysIcon }
      if MDIItemKind = mikSysMenu then
      begin
        ID := false;
        if TForm((ActiveMDIForm as TTeCustomForm).Form).Icon.Handle <> 0 then
          IHandle := TForm((ActiveMDIForm as TTeCustomForm).Form).Icon.Handle
        else
          if Application.Icon.Handle <> 0 then
            IHandle := Application.Icon.Handle
          else
          begin
            IHandle := LoadIcon(0, IDI_APPLICATION);
            ID := true;
          end;
        IconX := GetSystemMetrics(SM_CXSMICON);
        if IconX = 0 then IconX := GetSystemMetrics(SM_CXSIZE);
        IconY := GetSystemMetrics(SM_CYSMICON);
        if IconY = 0 then IconY := GetSystemMetrics(SM_CYSIZE);
        IHandle2 := CopyImage(IHandle, IMAGE_ICON, IconX, IconY, LR_COPYFROMRESOURCE);

        R := Classes.Rect(0, 0, 16, 16);
        RectCenter(R, Rect);
        DrawIconEx(Canvas.Handle, R.Left, R.Top, IHandle2, 0, 0, 0, 0, DI_NORMAL);

        DestroyIcon(IHandle2);
        if ID then DestroyIcon(IHandle);
      end;

      Exit;
    end;

    if View.IsMenuBar then
      FThemeEngine.Theme.DrawMenuBarItem(Canvas, Self, Rect, Active, Hover)
    else
      FThemeEngine.Theme.DrawMenuItem(Canvas, Self, Rect, Active, Hover)
  end
  else
    inherited;
end;

procedure TTeThemeItem.DrawScrollButton(Canvas: TCanvas; View: TTeItemView;
  Rect: TRect; Button: TTeMenuScrollButton; Active: boolean);
begin
  if not UseTheme then
    inherited
  else
  begin
    FThemeEngine.Theme.DrawMenuScrollButton(Canvas, Rect, Button, Active);
  end;
end;

{ Properties }

procedure TTeThemeItem.SetThemeEngine(const Value: TTeThemeEngine);
var
  i: integer;
begin
  FThemeEngine := Value;

  if Count > 0 then
    for i := 0 to Count-1 do
      TTeThemeItem(Items[i]).ThemeEngine := Value;
end;

initialization
  RegisterClasses([TTeThemeItem]);
finalization
end.
