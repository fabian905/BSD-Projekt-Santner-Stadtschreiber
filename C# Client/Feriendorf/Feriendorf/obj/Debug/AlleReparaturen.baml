   M S B A M L   `   `   `  ����E  AFeriendorf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Feriendorf
Feriendorf  5   	        Feriendorf.AlleReparaturen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Feriendorf     5   	     � Title$  AlleReparaturen��5   	   $��419.675��6!   $��642.921��62   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5
      $
��Left=�6%   $��	10,68,0,0q�6@   $	��Top=�6S   $	��284��6k   $	��588��6x   
�������   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ReparaturID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *ReparaturID+6Z     5      $ Materialaufwand��6%   $ * 6b    6>   �� *Materialaufwand+6b     5      $ 	Repariert��6%   $ * 6V    68   �� *	Repariert+6V     5      $ Notiz��6%   $ * 6N    64   �� *Notiz+6N     5      $ Beheber��6%   $ * 6R    66   �� *	Beheber+6R   5      �� 5   
   -   5         ��Name$ 	bttnClose��5      $��
Schließen��6$   $
��Left=�68   $��
505,20,0,0q�6S   $	��Top=�6g   $��93��6   6�   �� 5   
   -   5         ��Name$ label��5      $"��Übersicht: Alle Reparaturen��6   $
��Left=�6E   $��	10,10,0,0q�6`   $	��Top=�6s   $	��302��6�   $
��Bold"�6�   $��20(�6�   5      5      