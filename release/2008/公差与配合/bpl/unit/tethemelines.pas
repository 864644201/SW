{==============================================================================

  Lines theme
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: tethemelines.pas,v 1.1.1.1 2002/08/05 11:50:34 Evgeny Exp $

===============================================================================}

unit tethemelines;

{$I te_define.inc}

interface

uses Windows, Messages, Sysutils, Classes, Graphics, Controls, Menus,
  Grids, te_controls, KsThemeThemes, TeThemeHighlight;

type

{ TTeThemeLines class }

  TTeThemeLines = class(TTeThemeHighlight)
  private
  protected
  public
    function GetColor(Color: TTeThemeColor): TColor; override;

    procedure DrawWindow(Canvas: TCanvas; Width, Height: integer;
      ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle); override;
{    procedure DrawMenuBarItem(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); override;
    procedure DrawMenuItem(Canvas: TCanvas; Item: TTeCustomItem;
      Rect: TRect; Active, Hover: boolean); override; }

    { Return theme's name (need for selecting in IDE) }
    class function GetThemeName: string; override;
  published
  end;

implementation {===============================================================}

uses KsThemeEngine;

{ TTeThemeLines }

{ Metrix ======================================================================}

function TTeThemeLines.GetColor(Color: TTeThemeColor): TColor;
begin
  case Color of
    { Forms }
    ngcCaption: Result := KColorToColor(ckHighlight);
    ngcInactiveCaption: Result := KColorToColor(ckBtnShadow);
    ngcCaptionShadow: Result := clBlack;
    ngcCaptionText: Result := clWhite;
    ngcInactiveCaptionText: Result := clWhite;
    ngcBorder: Result := KColorToColor(ckBorder);
    ngcWindow: Result := KColorToColor(ckWindow);
    ngcHighlight: Result := KColorToColor(ckHighlight);
    ngcBtnFace: Result := KColorToColor(ckBtnFace);
    ngcHotHighlight: Result := KColorToColor(ckHotHighlight);
    { Menus }
    ngcMenuBorder: Result := KColorToColor(ckBorder);
    ngcMenuBar: Result := KColorToColor(ckWindow);
    ngcMenuBarHighlight: Result := KColorToColor(ckHighlight);
    ngcMenuItem: Result := KColorToColor(ckWindow);
    ngcMenuItemHighlight: Result := KColorToColor(ckHighlight);
    ngcMenuBarText: Result := RGB(0, 0, 0);
    ngcMenuBarHighlightText: Result := RGB(255, 255, 255);
    ngcMenuItemText: Result := RGB(0, 0, 0);
    ngcMenuItemHighlightText: Result := RGB(255, 255, 255);
    ngcMenuItemDisabledText: Result := RGB(128, 128, 128);
    { Controls }
    ngcWindowText: Result := RGB(0, 0, 0);
    ngcPanelCaption: Result := RGB(149, 206, 255);
    ngcPanelCaption2: Result := RGB(49, 106, 197);
    ngcDisabled: Result := RGB(254, 254, 251);
    ngcDisabledBorder: Result := RGB(197, 197, 177);
  else
    Result := ckRed;
  end;

  Result := KColorToColor(ChangeHue(KColor(Result), DeltaHue));
  Result := KColorToColor(ChangeBrightness(KColor(Result), DeltaBrightness));
end;

class function TTeThemeLines.GetThemeName: string;
begin
  Result := 'Lines';
end;

procedure TTeThemeLines.DrawWindow(Canvas: TCanvas; Width,
  Height: integer; ClientRect: TRect; Active: boolean; BorderStyle: TTeBorderStyle);
var
  R, BorderRect: TRect;
  CaptionRect: TRect;
  Color: TKColor;
  i: integer;
begin
  CaptionRect := ClientRect;
  CaptionRect.Bottom := CaptionRect.Top;
  CaptionRect.Top := ClientRect.Left;
  { Draw Caption }
  if Active then
    Color := GetColor(ngcCaption)
  else
    Color := GetColor(ngcInactiveCaption);

  FillRect(Canvas, CaptionRect, Color);
  for i := 0 to RectHeight(CaptionRect) div 2 do
  begin
    R := CaptionRect;
    R.Top := i * 3;
    R.Bottom := R.Top + 1;
    FillRect(Canvas, R, clBtnShadow);
  end;
  DrawRoundRect(Canvas, CaptionRect, 3, GetColor(ngcBorder));

  { Draw Border }
  BorderRect := Rect(0, 0, Width, Height);

  DrawRoundRect(Canvas, BorderRect, 3, GetColor(ngcBorder));
  InflateRect(BorderRect, -1, -1);
  DrawEdge(Canvas, BorderRect, RaisedColor(GetColor(ngcBtnFace), 30), SunkenColor(GetColor(ngcBtnFace), 30));
  InflateRect(BorderRect, -1, -1);
  DrawEdge(Canvas, BorderRect, RaisedColor(GetColor(ngcBtnFace), 15), SunkenColor(GetColor(ngcBtnFace), 15));
  InflateRect(BorderRect, -1, -1);
  DrawRect(Canvas, BorderRect, GetColor(ngcBtnFace)); 
end;

initialization
  RegisterTheme(TTeThemeLines);
end.




