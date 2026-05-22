{==============================================================================

  ThemeEngine
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeengine.pas,v 1.2 2002/08/10 00:57:28 Evgeny Exp $

===============================================================================}

unit ksthemeengine;

{$I te_define.inc}

interface

uses Windows, Forms, Messages, Sysutils, Classes, Graphics, Controls,
  te_controls, ksthemeversion, ksthemethemes;

type

{ TTeThemeEngine class }

{ Base theme's support component. Use this component for link controls or forms with specifies theme. }
  TTeThemeEngine = class(TComponent)
  private
    FTheme: TTeTheme;
    FDeltaHue: integer;
    FDeltaBrightness: integer;
    FColorAutoChanging: boolean;
    procedure SetTheme(const Value: TTeTheme);
    procedure ReadData(Stream: TStream);
    procedure writeData(Stream: TStream);
    function GetVersion: TTeThemeVersion;
    procedure SetVersion(const Value: TTeThemeVersion);
    function GetDeltaHue: integer;
    procedure SetDeltaHue(const Value: integer);
    procedure SetDeltaBrightness(const Value: integer);
    procedure SetColorAutoChanging(const Value: boolean);
  protected
    procedure DefineProperties(Filer: TFiler); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure Loaded; override;

    procedure ChangeTheme(ThemeClass: TTeThemeClass);
    procedure UpdateForms;
    procedure RepaintForms;
  published
    property ColorAutoChanging: boolean read FColorAutoChanging write SetColorAutoChanging;
    property DeltaBrightness: integer read FDeltaBrightness write SetDeltaBrightness;
    property DeltaHue: integer read FDeltaHue write SetDeltaHue;
    { Specifies the current theme. Change this property to change the theme's for all controls. }
    property Theme: TTeTheme read FTheme write SetTheme;
    { Use native Window XP theme }
    property Version: TTeThemeVersion read GetVersion write SetVersion;
  end;

const
  ThemesList: TList = nil;

{ Use this procedure to make ThemeClass visible in TTeThemeEngine component and available to use in application.

  For expamle you can call this in initialization section:

  <CODE>
  ...
  uses NgEngine;
  ...
  initialization
    RegisterTheme(TMyThemeClass);
  end;
  </CODE>
}
procedure RegisterTheme(Theme: TTeThemeClass);

implementation {===============================================================}

uses ksthemeforms, tethemeflat, tethemehighlight, tethemelines, tethemeluna,
  tethememetalluna, tethemestd, tethemecoolflat, tethemeofficexp, ksthemeswitch,
  tethemekde;

procedure RegisterTheme(Theme: TTeThemeClass);
begin
  if ThemesList = nil then
    ThemesList := TList.Create;

  ThemesList.Add(Theme);
end;

function ReadString(S: TStream): string;
var
  L: integer;
begin
  L := 0;
  S.Read(L, SizeOf(L));
  SetLength(Result, L);
  S.Read(Pointer(Result)^, L);
end;

procedure WriteString(S: TStream; Value: string);
var
  L: integer;
begin
  L := Length(Value);
  S.write(L, SizeOf(L));
  S.write(Pointer(Value)^, L);
end;

{ TTeThemeEngine }

constructor TTeThemeEngine.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);

  FColorAutoChanging := true;
  
  FTheme := TTeTheme.Create;
end;

destructor TTeThemeEngine.Destroy;
begin
  FTheme.Free;

  inherited Destroy;
end;

procedure TTeThemeEngine.Loaded;
begin
  inherited;
end;

procedure TTeThemeEngine.ChangeTheme(ThemeClass: TTeThemeClass);
var
  i: integer;
begin
  for i := 0 to ThemesList.Count-1 do
    if TTeThemeClass(ThemesList[i]) = ThemeClass then
    begin
      if FTheme <> nil then
        FTheme.Free;

      FTheme := TTeThemeClass(ThemesList[i]).Create;

      Break;
    end;

  if FTheme.UseTheme then
    FTheme.DeltaHue := FDeltaHue;
  if FTheme.UseTheme then
    FTheme.DeltaBrightness := FDeltaBrightness;

  UpdateForms;
end;

{ Custom property }

procedure TTeThemeEngine.DefineProperties(Filer: TFiler);
begin
  inherited;
  Filer.DefineBinaryProperty('ThemeClass', ReadData, writeData, true);
end;

procedure TTeThemeEngine.ReadData(Stream: TStream);
var
  S: string;
  i: integer;
begin
  try
    S := ReadString(Stream);
    for i := 0 to ThemesList.Count-1 do
      if TTeThemeClass(ThemesList[i]).GetThemeName = S then
      begin
        ChangeTheme(TTeThemeClass(ThemesList[i]));
        Break;
      end;
  except
  end;
end;

procedure TTeThemeEngine.WriteData(Stream: TStream);
begin
  if FTheme <> nil then
    writeString(Stream, FTheme.GetThemeName);
end;

procedure TTeThemeEngine.UpdateForms;
var
  i, j: integer;
  R: TRect;
begin
  for i := 0 to Screen.FormCount - 1 do
  begin
    if FColorAutoChanging then
      if FTheme.UseTheme and not (csDesigning in ComponentState) then
        Screen.Forms[i].Color := FTheme.GetColor(ngcWindow)
      else
        Screen.Forms[i].Color := clBtnFace;

    for j := 0 to Screen.Forms[i].ComponentCount - 1 do
    begin
      if (Screen.Forms[i].Components[j] is TTeThemeForm) and
         ((Screen.Forms[i].Components[j] as TTeThemeForm).ThemeEngine <> nil) and
         ((Screen.Forms[i].Components[j] as TTeThemeForm).ThemeEngine = Self) then
      begin
        (Screen.Forms[i].Components[j] as TTeThemeForm).Update;
        (Screen.Forms[i].Components[j] as TTeThemeForm).UpdateForm;
        (Screen.Forms[i].Components[j] as TTeThemeForm).UpdateControls;
      end;

      if Screen.Forms[i].Components[j] is TTeSwitcherComboBox then
        if TTeSwitcherComboBox(Screen.Forms[i].Components[j]).ThemeEngine = Self then
          TTeSwitcherComboBox(Screen.Forms[i].Components[j]).ThemeEngine := Self; 
    end;

    if Screen.Forms[i].FormStyle = fsMDIForm then
    begin
      R := Classes.Rect(0, 0, Screen.Width, Screen.Height);
      InvalidateRect(Screen.Forms[i].ClientHandle, @R, true);
    end;
  end;
end;

procedure TTeThemeEngine.RepaintForms;
var
  i, j: integer;
begin
  for i := 0 to Screen.FormCount - 1 do
  begin
    if not Screen.Forms[i].Visible then Continue;
    
    for j := 0 to Screen.Forms[i].ComponentCount - 1 do
      if Screen.Forms[i].Components[j] is TTeThemeForm then
      begin
        (Screen.Forms[i].Components[j] as TTeThemeForm).UpdateForm;
        (Screen.Forms[i].Components[j] as TTeThemeForm).UpdateControls;
      end;
  end;
end;

{ Properties }

procedure TTeThemeEngine.SetTheme(const Value: TTeTheme);
begin
  FTheme := Value;
  UpdateForms;
end;

function TTeThemeEngine.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeEngine.SetVersion(const Value: TTeThemeVersion);
begin
end;

function TTeThemeEngine.GetDeltaHue: integer;
begin
  Result := FDeltaHue;
end;

procedure TTeThemeEngine.SetDeltaHue(const Value: integer);
begin
  if FDeltaHue <> Value then
  begin
    FDeltaHue := Value;
    if FTheme.UseTheme then
    begin
      FTheme.DeltaHue := Value;
      if not (csLoading in ComponentState) then
        UpdateForms;
    end;
  end;
end;

procedure TTeThemeEngine.SetDeltaBrightness(const Value: integer);
begin
  if FDeltaBrightness <> Value then
  begin
    FDeltaBrightness := Value;
    if FTheme.UseTheme then
    begin
      FTheme.DeltaBrightness := Value;
      if not (csLoading in ComponentState) then
        UpdateForms;
    end;
  end;
end;

procedure TTeThemeEngine.SetColorAutoChanging(const Value: boolean);
begin
  FColorAutoChanging := Value;
end;

initialization
finalization
  if ThemesList <> nil then ThemesList.Free;
  ThemesList := nil;
end.




