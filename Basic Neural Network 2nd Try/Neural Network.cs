using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Neural_Network_2nd_Try
{
    class Neural_Network
    {
        private int NumOfinput, NumOfhidden, NumOfoutput;
        private double lr;
        private Matrix_Math Winput_hidden, Whidden_output, bias_hidden, bias_output;
        public Neural_Network(double lr, int input, int hidden, int output)
        {
            // Defines the basic information of a neural network: number of input nodes, hidden nodes, output nodes and learning rate
            this.NumOfinput = input;
            this.NumOfhidden = hidden;
            this.NumOfoutput = output;
            this.lr = lr;
            //creates a matrix for the weights between the input and hidden in the size of hidden*input
            this.Winput_hidden = new Matrix_Math(NumOfhidden, NumOfinput);
            //creates a matrix for the weights between the hidden and output in the size of output*hidden
            this.Whidden_output = new Matrix_Math(NumOfoutput, NumOfhidden);

            //randomises the matirxes values from -0.5 to 0.5
            this.Winput_hidden.RandomiseSelf(5);
            this.Whidden_output.RandomiseSelf(5);

            //Create the biases and randomises them for the hidden and output layers
            bias_hidden = new Matrix_Math(NumOfhidden, 1);
            bias_hidden.RandomiseSelf(5);
            bias_output = new Matrix_Math(NumOfoutput, 1);
            bias_output.RandomiseSelf(5);
            //this.input_hidden.PrintSelf();
            //this.hidden_output.PrintSelf();
        }

        public void train(double[,] inputs_array, double[,] targets_array)
        {
            //converts the array into a useable matrices object
            Matrix_Math inputs = new Matrix_Math(inputs_array);
            Matrix_Math targets = new Matrix_Math(targets_array);


            //Multiplie the input-hidden weight matrix with the inputs to create the input of the hidden layer
            Matrix_Math hidden_inputs = Matrix_Math.multiply(this.Winput_hidden, inputs);

            //adds the bias
            hidden_inputs.AddSelf(bias_hidden);

            //Sigmoids the input of the hidden layer to create the output of the hidden layer
            Matrix_Math hidden_outputs = Matrix_Math.Map(hidden_inputs, sigmoid);

            //Multiplie the hidden-output weight matrix with the output of the hidden layer to create the input of the final layer
            Matrix_Math final_inputs = Matrix_Math.multiply(this.Whidden_output, hidden_outputs);

            //adds the bias
            final_inputs.AddSelf(bias_output);

            //Sigmoids the input of the hidden layer to create the output of the final layer
            Matrix_Math final_outputs = Matrix_Math.Map(final_inputs, sigmoid);

            //Calculates the error for the output layer - (target - actual)
            Matrix_Math output_errors = Matrix_Math.Subtract(targets, final_outputs);

            //Calculates the errors of the hidden layer by multiplying the transposed weights of the hidden - output layer by the errors of the output layer
            Matrix_Math hidden_errors = Matrix_Math.multiply(Matrix_Math.Transpose(this.Whidden_output), output_errors);

            //OOF, this multiples the learning rate by the dot product of the multipication of the output erros, the output of the final layer and (1 - the output of the final layer) and the transposed output of the hidden layer
            this.Whidden_output.AddSelf(Matrix_Math.multiply(Matrix_Math.multiply(Matrix_Math.MultiplyHadamard(Matrix_Math.MultiplyHadamard(output_errors, final_outputs), Matrix_Math.Map(final_outputs, dsigmoid)), Matrix_Math.Transpose(hidden_outputs)), this.lr));

            //OOF, this multiples the learning rate by the dot product of the multipication of the hidden erros, the output of the hiddedn layer and (1 - the output of the hidden layer) and the transposed output of the input layer
            this.Winput_hidden.AddSelf(Matrix_Math.multiply(Matrix_Math.multiply(Matrix_Math.MultiplyHadamard(Matrix_Math.MultiplyHadamard(hidden_errors, hidden_outputs), Matrix_Math.Map(hidden_outputs, dsigmoid)), Matrix_Math.Transpose(inputs)), this.lr));

            //updates the bias like the weights except it doesn't use the dot product of the transposed hidden layet
            this.bias_output.AddSelf(Matrix_Math.multiply(Matrix_Math.MultiplyHadamard(/*Matrix_Math.MultiplyHadamard(*/output_errors/*, final_outputs)*/, Matrix_Math.Map(final_outputs, dsigmoid)), this.lr));

            this.bias_hidden.AddSelf(Matrix_Math.multiply(Matrix_Math.MultiplyHadamard(/*Matrix_Math.MultiplyHadamard(*/hidden_errors/*, hidden_outputs)*/, Matrix_Math.Map(hidden_outputs, dsigmoid)), this.lr));
        }

        public Matrix_Math query(double[,] inputs_array)
        {
            //converts the array into a useable matrix object
            Matrix_Math inputs = new Matrix_Math(inputs_array);
            //Multiplie the input-hidden weight matrix with the inputs to create the input of the hidden layer
            Matrix_Math hidden_inputs = Matrix_Math.multiply(this.Winput_hidden, inputs);
            //Sigmoids the input of the hidden layer to create the output of the hidden layer

            //adds the bias
            hidden_inputs.AddSelf(bias_hidden);

            Matrix_Math hidden_outputs = Matrix_Math.Map(hidden_inputs, sigmoid);
            //Multiplie the hidden-output weight matrix with the output of the hidden layer to create the input of the final layer
            Matrix_Math final_inputs = Matrix_Math.multiply(this.Whidden_output, hidden_outputs);

                        //adds the bias
            final_inputs.AddSelf(bias_output);

            //Sigmoids the input of the hidden layer to create the output of the final layer
            Matrix_Math final_outputs = Matrix_Math.Map(final_inputs, sigmoid);

            return final_outputs;
        }

        public static double sigmoid(double x, int rows, int cols)
        {
            return (Math.Pow(Math.E, x)) / (Math.Pow(Math.E, x) + 1);
        }

        public static double dsigmoid(double x, int rows, int cols)
        {
            return x * (1 - x); // sigmoid(x) * (1-sigmoid(x))
        }


    }
}
