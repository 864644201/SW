{==============================================================================

  ThemeEngine's Header
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeheader.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthemeheader;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms,
  te_controls, KsThemeThemes, KsThemeEngine, KsThemeVersion;

type

{ TTeThemeHeaderControl class }

  TTeThemeHeaderControl = class(TTeCustomHeaderControl)
  private
    FThemeEngine: TTeThemeEngine;
    function GetVersion: TTeThemeVersion;
    procedure SetThemeEngine(const Value: TTeThemeEngine);
    procedure SetVersion(const Value: TTeThemeVersion);
  protected
    function UseTheme: boolean;
    { overrides }
    procedure DrawSection(Section: TTeHeaderSection; ARect: TRect;
      AState: TTeSectionState); override;
    procedure DrawTailSection(ARect: TRect); override;
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

{ TTeThemeHeaderControl ===============================================================}

constructor TTeThemeHeaderControl.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemeHeaderControl.Destroy;
begin
  inherited Destroy;
end;

function TTeThemeHeaderControl.UseTheme: boolean;
begin
  if (csDestroying in ComponentState) or (csLoading in ComponentState) then
    Result := false
  else
    Result := (FThemeEngine <> nil) and (FThemeEngine.Theme <> nil) and
      (FThemeEngine.Theme.UseTheme);
end;

procedure TTeThemeHeaderControl.DrawSection(Section: TTeHeaderSection;
  ARect: TRect; AState: TTeSectionState);
var
  Offset: TPoint;
begin
  if UseTheme then
  begin
    FThemeEngine.Theme.DrawHeaderSection(Canvas, ARect, Section, AState);

    Offset := Point(0,0);
    if AState in [ssPressed, ssDraggedOut, ssUnderDrag] then Offset := Point(1,1);

    if Section.Style = hsOwnerDraw then
      DoDrawSection(Self, Section, ARect, AState)
    else
    begin
      DrawSectionText(Section, ARect, Offset, AState);
      if Section.ShowImage then
        DrawSectionImage(Section, ARect, Offset, AState);
    end;
  end
  else
    inherited;
end;

procedure TTeThemeHeaderControl.DrawTailSection(ARect: TRect);
begin
  if UseTheme then
  begin
    FThemeEngine.Theme.DrawHeaderSection(Canvas, ARect, nil, ssNormal);
  end
  else
    inherited;
end;

{ Properties }

function TTeThemeHeaderControl.GetVersion: TTeThemeVersion;
begin
  Result := sTeThemeVersion;
end;

procedure TTeThemeHeaderControl.SetThemeEngine(const Value: TTeThemeEngine);
begin
  FThemeEngine := Value;
  Invalidate;
end;

procedure TTeThemeHeaderControl.SetVersion(const Value: TTeThemeVersion);
begin

end;

end.
