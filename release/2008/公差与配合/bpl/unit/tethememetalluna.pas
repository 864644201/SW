{==============================================================================

  Luna Theme
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: tethememetalluna.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit tethememetalluna;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus, Grids,
  te_controls, KsThemeThemes, tethemeluna;

type

{ TTeThemeMetalLuna class }

  TTeThemeMetalLuna = class(TTeThemeLuna)
  private
  protected
    procedure ReloadBitmaps; override;
  public
    function GetColor(Color: TTeThemeColor): TColor; override;
    { Return theme's name (need for selecting in IDE) }
    class function GetThemeName: string; override;
  published
  end;

implementation {===============================================================}

uses KsThemeEngine;

{$R *.res}

var
  ResStream: TResourceStream;

{ TTeThemeMetalLuna }

procedure TTeThemeMetalLuna.ReloadBitmaps;
begin
  if InactiveWindow <> nil then
    InactiveWindow.Free;
  if ActiveWindow <> nil then
    ActiveWindow.Free;
  { }
  ActiveWindow := TTeBitmap.Create;
  ResStream := TResourceStream.Create(HInstance, PChar('XP_METAL_MAIN'), RT_RCDATA);
  try
    ActiveWindow.LoadFromPcxStream(ResStream);
    if not ActiveWindow.Empty then
      ActiveWindow.PerformTransparent(ActiveWindow.Pixels[0, 0]);
  finally
    ResStream.Free;
  end;
  { }
  InactiveWindow := TTeBitmap.Create;
  ResStream := TResourceStream.Create(HInstance, PChar('XP_METAL_MAIN1'), RT_RCDATA);
  try
    InactiveWindow.LoadFromPcxStream(ResStream);
    if not InactiveWindow.Empty then
      InactiveWindow.PerformTransparent(InactiveWindow.Pixels[0, InactiveWindow.Height - 1]);
  finally
    ResStream.Free;
  end;
end;

{ Metrix ======================================================================}

function TTeThemeMetalLuna.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaptionText: Result := RGB(0,0,128);
    ngcInactiveCaptionText: Result := RGB(128, 128, 128);
    ngcCaptionShadow: Result := clWhite;
    ngcBorder: Result := RGB(163, 163, 184);
    ngcWindow: Result := RGB(248, 248, 255);
    ngcHighlight: Result := RGB(90, 90, 123);
    ngcBtnFace: Result := RGB(224, 223, 228);
    { Menus }
    ngcMenuBorder: Result := RGB(128, 128, 128);
    ngcMenuBar: Result := RGB(255, 255, 255);
    ngcMenuBarHighlight: Result := RGB(163, 163, 184);
    ngcMenuItem: Result := RGB(255, 255, 255);
    ngcMenuItemHighlight: Result := RGB(163, 163, 184);
    ngcMenuBarText: Result := RGB(0, 0, 0);
    ngcMenuBarHighlightText: Result := RGB(255, 255, 255);
    ngcMenuItemText: Result := RGB(0, 0, 0);
    ngcMenuItemHighlightText: Result := RGB(255, 255, 255);
    ngcMenuItemDisabledText: Result := RGB(128, 128, 128);
    { Controls }
    ngcWindowText: Result := RGB(0, 0, 0);
    ngcPanelCaption: Result := RGB(230, 230, 240);
    ngcPanelCaption2: Result := RGB(163, 163, 173);
    ngcDisabled: Result := RGB(254, 254, 251);
    ngcDisabledBorder: Result := RGB(197, 197, 177);
  else
    Result := ckRed;
  end;

  Result := KColorToColor(ChangeHue(KColor(Result), DeltaHue));
  Result := KColorToColor(ChangeBrightness(KColor(Result), DeltaBrightness));
end;

class function TTeThemeMetalLuna.GetThemeName: string;
begin
  Result := 'MetalLuna';
end;

initialization
  RegisterTheme(TTeThemeMetalLuna);
end.




