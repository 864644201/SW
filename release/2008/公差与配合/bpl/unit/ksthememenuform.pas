{==============================================================================

  ThemeEngine Items
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthememenuform.pas,v 1.1.1.1 2002/08/05 11:50:33 Evgeny Exp $

===============================================================================}

unit ksthememenuform;

{$I te_define.inc}
{$T-,W-,X+,P+}

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, ExtCtrls,
  te_controls, ksthemethemes, ksthemeengine;

type

{ TTeThemePopupForm }

  TTeThemePopupForm = class(TTePopupForm)
  private
  protected
    procedure PaintNonClientArea(Canvas: TCanvas); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
  end;

{ TTeThemePopupWindow class }

  TTeThemePopupWindow = class(TTePopupWindow)
  private
    FThemeEngine: TTeThemeEngine;
  protected
    class function GetFormClass: TTePopupFormClass; override;
  public
    constructor CreatePopupWindow(AOwner: TComponent; AItems: TTeCustomItem;
      AParentView: TTeItemView); override;
    destructor Destroy; override;
    { Properties }
    property ThemeEngine: TTeThemeEngine read FThemeEngine write FThemeEngine;
  end;

implementation {===============================================================}

uses ksthemeitems;

{ TTeThemePopupForm ================================================================}

constructor TTeThemePopupForm.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

destructor TTeThemePopupForm.Destroy;
begin
  inherited;
end;

procedure TTeThemePopupForm.PaintNonClientArea(Canvas: TCanvas);
begin
  if (PopupWindow <> nil) and (TTeThemePopupWindow(PopupWindow).ThemeEngine <> nil) and
     (TTeThemePopupWindow(PopupWindow).ThemeEngine.Theme <> nil) and
     (TTeThemePopupWindow(PopupWindow).ThemeEngine.Theme.UseTheme)
  then
    with TTeThemePopupWindow(PopupWindow).ThemeEngine.Theme do
    begin
      DrawPopupMenu(Canvas, Width, Height);
    end
  else
    inherited ;
end;

{ TTeThemePopupWindow ===============================================================}

constructor TTeThemePopupWindow.CreatePopupWindow(AOwner: TComponent;
  AItems: TTeCustomItem; AParentView: TTeItemView);
begin
  inherited CreatePopupWindow(AOwner, AItems, AParentView);

  if AItems <> nil then
    FThemeEngine := TTeThemeItem(AItems).ThemeEngine;
end;

destructor TTeThemePopupWindow.Destroy;
begin
  inherited Destroy;
end;

class function TTeThemePopupWindow.GetFormClass: TTePopupFormClass;
begin
  Result := TTeThemePopupForm;
end;

end.
