require File.dirname(__FILE__) + './bin/Debug/Kona.Web.dll'

require 'rubygems'
require 'spec'

  describe ShoppingCart, "defines the shopping cart" do
   
    before(:each) do  
      @cart =ShoppingCart.new 
    end

    it "should exist" do
      @cart.should == @cart
    end
    
  end
