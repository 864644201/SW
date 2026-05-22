{==============================================================================

  Shared Routines for KSDev's products
  Copyright (C) 2000-2002 by Evgeny Kryukov
  All rights reserved

  All contents of this file and all other files included in this archive
  are Copyright (C) 2002 Evgeny Kryukov. Use and/or distribution of
  them requires acceptance of the License Agreement.

  See License.txt for licence information

  $Id: te_shared.pas,v 1.10.2.1 2003/01/23 16:14:46 evgeny Exp $

===============================================================================}

unit te_shared;

{$I te_define.Inc}
{$T-,W-,X+,P+}

interface

uses Messages, Controls;

const

  ksSharedVersion: string = '1.0.0';

{ Paint to bitmap message - paint form content to specified DC
  wParam - Point
  lParam - DC }

  CM_PAINTTOBITMAP      = CM_BASE + 456;


implementation {===============================================================}

end.
