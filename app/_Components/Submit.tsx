const Submit = ({ handleClose }: { handleClose: () => void }) => {
   return (
      <button
         onClick={handleClose}
         className="flex w-1/2 items-center h-12 justify-center rounded-3xl text-sm tracking-widest border bg-orange-600 hover:bg-white hover:text-orange-600 hover:border-orange-600 transition-colors duration-500"
      >
         SUBMIT
      </button>
   )
}

export default Submit
