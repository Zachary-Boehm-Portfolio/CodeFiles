package application;
	
import java.util.Scanner;

import com.sun.prism.paint.Color;

import javafx.application.Application;
import javafx.stage.Stage;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.layout.BorderPane;
import javafx.scene.shape.Line;


public class Main extends Application {
	int counter_X = 0;
	Coordinates [] coordinates = new Coordinates [1000];
	Group Equation = new Group();
	@Override
	public void start(Stage primaryStage) {
		try {
			BorderPane root = new BorderPane();
			Scene scene = new Scene(root,400,400);
			
		//>--------------------------------------------------------------------------
			root.setCenter(Equation);
			Scanner input = new Scanner(System.in);
		//>--------------------------------------------------------------------------
			Draw();
			
			
			scene.getStylesheets().add(getClass().getResource("application.css").toExternalForm());
			
			primaryStage.setScene(scene);
			primaryStage.show();
			input.close();
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	public static void main(String[] args) {
		launch(args);
	}
	public double FindY() {
		double Y_Value = equation(counter_X);
		counter_X++;
		return  Y_Value;
	}
	public double equation(int X) {
		double Y_value;
		Y_value = 3 * X;
		return Y_value;
	}
	public void Draw() {
		for (int i = 0; i < 1000; i++) {
			double X = counter_X;
			double Y = FindY();
			Equation.getChildren().add(new Line(X, Y, X, Y));
		}
	}
}
