   M S B A M L   `   `   `  ����  Autobike    Autobike.InsuranceWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$1  +Управление страховками��5   	   $	��450��6(   $	��800��65   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5   
   " ��6+   .�5	      ��  � �� 5
      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � .+�5   
   �� -   5        $��X��5      $��30��6   $	��Top=�6)   $��Right=�6A   
������ ���#E74C3C
������ $
��Bold"�5   Y   $��0,5,5,0q�5   +   �� 5"   
   ���#2E2E3E$ 10��6&   $��10,50,10,60q�68   $	 10q�6M   ._�5#      �� $
��Auto=�6   .��5$      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 InsuranceDataGrid��5$         AutoGenerateColumns .  $��Stretch=�6R   $��Stretch=�6p      
IsReadOnly .    SelectionMode$ Single=�5%   -   
������ 
�������   RowBackground �#3A3A3A   AlternatingRowBackground �#2E2E2E   GridLinesVisibility$
 None=�5'      
������    Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6P      MinWidth$ 80Q�6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *id+6\     5*      $ Дата начала��6-   $	 200 6a   $	 150Q�6m    6B   �� *
start_date+6m     5+      $! Дата окончания��6-   $	 200 6b   $	 150Q�6n    6E   �� *
end_date+6n   4  .System.Windows.Controls.DataGridComboBoxColumn  5,      $% Номер автомобиля��61   $	 200 6v   $	 150Q�6�      SelectedItemBinding 6K   �� *
car_number+6�      ColumnHeaderStyle 50      �� 51      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�52      ��  � �� 53   "   �� 54   &    �� 
StartPoint$	 0,0&�6:    �� EndPoint$	 1,0&�6K   .��55   *   �  � Color$ #4CAF50��67    � Offset$	 0.0Q�6G   � 56   *   $ #3D8B4E��67   $	 1.0Q�6G   57   '   59      �� 5:       � ��
 �����6;   �� 5;       � ��$ 16(�69   �� 5<       � ��$
 Bold"�6;   �� 5=       � ��$ Center=�6K   �� 5>       � ��$
 10,5q�68   �� 5?       � ��$ 50��67   �� 5A       �@��TextAlignment$ Center=�6H   �� 5B       � ��
 ���� 6<   �� 5C       � ��$ Center=�6B   5E      5G      5H      5I      �� 5L   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��	10,0,0,10q�6d   .+�5M      �� -   5        $��Добавить��5M      $	��100��6(   $��30��64   $��0,0,10,0q�6@   ���#4CAF50
����������#4CAF50$��1q�6�   #��[  6�   6�   �� 5N      -   5        $ ��Редактировать��5N      $	��100��6-   $��30��69   $��0,0,10,0q�6E   
������ ���#4CAF50���#4CAF50$��1q�6�   #��[  6�   6�   �� 5O      -   5        $��Удалить��5O      $	��100��6'   $��30��63   
������ 
���  ��
���  ��$��1q�6{   #��[  6�   6�   5P      5Q      5R      