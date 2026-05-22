{==============================================================================

  ThemeEngine Version
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All conTeThements of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: ksthemeversion.pas,v 1.11.2.2 2003/01/23 16:14:45 evgeny Exp $

===============================================================================}

unit ksthemeversion;

interface

{$I te_define.inc}

const
  sTeThemeVersion = '3.3.1';
  sTeThemeVersionPropText = 'ThemeEngine version ' + sTeThemeVersion;

type
  TTeThemeVersion = type string;

const
  Sig: PChar = '- ' + sTeThemeVersionPropText +
    {$IFDEF KS_CBUILDER4} ' - CB4 - ' + {$ENDIF}
    {$IFDEF KS_DELPHI4} ' - D4 - '+ {$ENDIF}
    {$IFDEF KS_CBUILDER5} ' - CB5 - '+ {$ENDIF}
    {$IFDEF KS_DELPHI5} ' - D5 - '+ {$ENDIF}
    {$IFDEF KS_CBUILDER6} ' - CB6 - '+ {$ENDIF}
    {$IFDEF KS_DELPHI6} ' - D6 - '+ {$ENDIF}
    {$IFDEF KS_CBUILDER7} ' - CB7 - '+ {$ENDIF}
    {$IFDEF KS_DELPHI7} ' - D7 - '+ {$ENDIF}
    'Copyright (C) 2000-2002 by Evgeny Kryukov -';

procedure ShowVersion;
procedure ShowVersion2;

implementation {===============================================================}

uses Forms, Dialogs, SysUtils; 

procedure ShowVersion;
const
  AboutText =
    '%s'#13#10 +
    'Copyright (C) 2001-2002 by Evgeny Kryukov'#13#10 +
    'For conditions of distribution and use, see LICENSE.TXT.'#13#10 +
    #13#10 +
    'Visit our web site for the latest versions of ThemeEngine:'#13#10 +
    'http://www.ksdev.com/';
begin
  MessageDlg(Format(AboutText, [sTeThemeVersionPropText]), mtInformation, [mbOK], 0);
end;

procedure ShowVersion2;
const
  AboutText =
    'This application uses a trial version of ThemeEngine.'#13#10#13#10 +
    'Please contact the provider of the application for a registered version.'#13#10 +
    #13#10+
    '%s'#13#10 +
    'Copyright (C) 2001-2002 by Evgeny Kryukov'#13#10 +
    #13#10 +
    'For conditions of distribution and use, see LICENSE.TXT.'#13#10 +
    #13#10 +
    'Visit our web site for the latest versions of ThemeEngine:'#13#10 +
    #13#10 +
    'http://www.ksdev.com/' +
    #13#10 +
    'support@ksdev.com';
var
  X, Y: integer;
begin
  X := Random(Screen.Width - 400);
  Y := Random(Screen.Height - 200);
  MessageDlgPos(Format(AboutText, [sTeThemeVersionPropText]), mtInformation, [mbOK], 0, X, Y);
end;

initialization
  Sig := Sig;
end.
