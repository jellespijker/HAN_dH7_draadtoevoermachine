function  CurrentOutput = SimulateTF( num, den, PreviousValue, CurrentValue )
% SimulateTF 
% Outputs a Double value from a simulated transfer function in the S-
% domain. 
%
% The MIT License (MIT)
% 
% Copyright (c) 2013 Jelle Spijker
% 
% Permission is hereby granted, free of charge, to any person obtaining a copy
% of this software and associated documentation files (the "Software"), to deal
% in the Software without restriction, including without limitation the rights
% to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
% copies of the Software, and to permit persons to whom the Software is
% furnished to do so, subject to the following conditions:
% 
% The above copyright notice and this permission notice shall be included in
% all copies or substantial portions of the Software.
% 
% THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
% IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
% FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
% AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
% LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
% OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
% THE SOFTWARE.
%
% Input argumenst are:
% num =             Numerator of the transfer function. Array of doubles. Polynomial
%                   ordered from nth... 2nd... 1th... Constant
% den =             Denumerator of the transfer function. Array of doubles. Polynomial
%                   ordered from nth... 2nd... 1th... Constant
% PreviousValue =   Value from the previous itteration. Use the shift register
%                   to obtain this value
% CurrentValue =    Value from the current itteration. 
%
% Output =          Current simulated value

TransferFunction = tf(num, den); % Init a transferfunction
TimeVector = 0:1; % Make a Time vector

% Check if previous value and current value deviate from each other so
% that it allways outputs an array with 2 columns
if PreviousValue ~= CurrentValue 
    InputVector = ones(1,2) * PreviousValue;
    InputVector(2) = CurrentValue;
else
    InputVector = ones(1,2) * PreviousValue;
end

%Simulate the input values with the current transfer function and outputs the results 
OutputVector = lsim(TransferFunction, InputVector, TimeVector);
CurrentOutput = OutputVector(end);
end

