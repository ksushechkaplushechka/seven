   M S B A M L   `   `   `  ����  Autobike    Autobike.TariffWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$+  %Управление тарифами��5   	   $	��450��6%   $	��800��62   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5   
   " ��6+   .�5	      ��  � �� 5
      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � .+�5   
   �� -   5        $��X��5      $��30��6   $��30��6(   $��Right=�64   $	��Top=�6P   
������ ���#E74C3C
������ $
��Bold"�5   Y   $��0,5,5,0q�5   +   �� 5"   
   $+��%Управление тарифами��6   $��Center=�6/   $	��Top=�6L   $��0,30,0,0q�5#      $��18(�6&   
�������64   �� 5&   
   ���#2E2E3E$ 10��6&   $��10,50,10,60q�68   $	 10q�6M   ._�5'      �� $
��Auto=�6   .��5(      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 TariffDataGrid��5(         AutoGenerateColumns .  $��Stretch=�6O   $��Stretch=�6m      
IsReadOnly . 
������ 
�������   RowBackground �#3A3A3A   AlternatingRowBackground �#2E2E2E   GridLinesVisibility$
 None=�5*      
������    ColumnHeaderStyle 5+      �� 5,      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5-      ��  � �� 5.   "   �� 5/   &    �� 
StartPoint$	 0,0&�6:    �� EndPoint$	 1,0&�6K   .��50   *   �  � Color$ #4CAF50��67    � Offset$	 0.0Q�6G   � 51   *   $ #3D8B4E��67   $	 1.0Q�6G   52   '   54      �� 55       � ��
 �����6;   �� 56       � ��$ 16(�69   �� 57       � ��$
 Bold"�6;   �� 58       � ��$ Center=�6K   �� 59       � ��$ 10q�68   �� 5:       � ��
 ���� 6<   5;         Columns 5=      0  *System.Windows.Controls.DataGridTextColumn  5>      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6U      MinWidth$	 100Q�6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ��  �� Path$ id�6b     5?      $# Название тарифа��6-   $
 Auto 6d   $	 150Q�6q    6F   �� $
 name�6q     5@      $ Цена��6-   $
 Auto 6j   $	 100Q�6w    6;   �� $ price� �� StringFormat$ C��6w   5B      5C      5D      �� 5G   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��	10,0,0,10q�6d   .+�5H      �� -   5        $��Добавить��5H      $	��100��6(   $��30��64   $��0,0,10,0q�6@   ���#4CAF50
����������#4CAF50$��1q�6�   #��[  6�   6�   �� 5I      -   5        $ ��Редактировать��5I      $	��100��6-   $��30��69   $��0,0,10,0q�6E   
������ ���#4CAF50���#4CAF50$��1q�6�   #��[  6�   6�   �� 5J      -   5        $��Удалить��5J      $	��100��6'   $��30��63   
������ 
���  ��
���  ��$��1q�6{   #��[  6�   6�   5K      5L      5M      