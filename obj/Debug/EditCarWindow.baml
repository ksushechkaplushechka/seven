   M S B A M L   `   `   `  ����  Autobike    Autobike.EditCarWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$5  /Редактировать автомобиль��5   	   $	��410��6*   $	��400��67   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5	   
   " ��6+   .�5
      ��  � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � $��20q�6   .+�5   
   �� -   5        $��X��5      $��30��6   $��30��6(   $��Right=�64   $	��Top=�6P   
������ ���#E74C3C
������ $
��Bold"�5   Y   $��0,5,5,0q�5    +   �� 5"   
   $	��Top=�6   $	��348��6-   $��0,20,0,0q�6:   .+�5$      �� $%��Номер автомобиля��6   $	��0,5q�60   
�������$��14(�6P   �� 5%      -   5        
 ��Name$
 CarNumberTextBox��5%      $	��300��60   ���#3A3A3A
�������
������� �� 
IsReadOnly . 6w   �� 5(      $%��Марка автомобиля��6   $��10,5,0,5q�60   
�������$��14(�6U   �� 5)      -   5         ��Name$ BrandComboBox��5)      $	��300��6.   ���#3A3A3A
���   � �� DisplayMemberPath$
 name��5*       �� SelectedValuePath$ id��60   �� 5-      $��Сотрудник��6   $��10,5,0,5q�6)   
�������$��14(�6N   �� 5.      -   5        $ EmployeeComboBox��5.      $	��300��61   ���#3A3A3A
���   �$ 	last_name��5/      $ id��65   �� 52      $��Год выпуска��6   $��10,5,0,5q�6+   
�������$��14(�6P   �� 53      -   5        $
 YearTextBox��53      $	��300��6+   ���#3A3A3A
�������
�������6_   �� 56      $��
Тариф��6   $��10,5,0,5q�6%   
�������$��14(�6J   �� 57      -   5        $ TariffComboBox��57      $	��300��6/   ���#3A3A3A
���   �$
 name��58      $ id��60   �� 5;      -   5         ��Name$ IsBookedCheckBox��5;      $ ��Забронировано��61   $��10q�6I   
�������6U   �� 5<      -   5        $ IsWorkCheckBox��5<      $��В работе��6/   $��10q�6B   
�������6N   5=      �� 5@   
   -	   5        $��Сохранить��5@      $	��100��6%   $��30��61   $��
0,330,0,10q�6=   $��Center=�6Q   ���#4CAF50
����������#4CAF50$��1q�5A   O   #��[  5B      6=   5C      5D      