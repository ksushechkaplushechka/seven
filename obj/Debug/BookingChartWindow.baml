   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	    Autobike!   Autobike.BookingChartWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � Title$  Booking Chart��5   	   $	��420��6   $	��600��6,   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5	   
   " ��6+   .�5
      ��  � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � .+�5   
   � ���#2E2E3E$��50��6$   $	��Top=�60   .+�5      �� $1��+Диаграмма бронирований��6   $��Center=�66   $
��Left=�6Q   $��20,0,0,0q�5       $��18(�6*   ���#FFFFFF$
��Bold"�6M   �� 5!      -   5        $��X��5!      #��[  6!   $��30��6M   $��30��6X   $��Right=�5"      $��Center=�61   $��0,0,10,0q�6L   ���#E74C3C���#E74C3C5#   @   5$      �� 5'   
   ���#2E2E3E$ 10��6&   $��20,70,20,26q�68   $	 10q�6M   ._�5(      #   LiveCharts.Wpf.CartesianChart  -   5        
  Name$
 BookingChart��5(   !   
������ &    LiveCharts.Wpf.Charts.Base.Chart   AxisX 5*         LiveCharts.Wpf.Axis     Title$ 
Месяц��6      
Foreground
 �����   FontSize$ 14Q�6@      
FontWeight$
 Bold"�6N      AxisY 5-        $3 -Количество бронирований��6   
 �����$ 14Q�6R   $
 Bold"�6`   5/      50      �� 53   
   $A��;© 2024 CarSharing Inc. Все права защищены.��6   $��Center=�6F   $��Bottom=�54      $��0,0,0,10q�6/   ���#555566$��12(�6V   55      56      