using WpfApp1.exia.ipc.entities;

namespace WpfApp1.exia.ipc.fail;

public class WrongStep3 : IStep3Strategy
{
    public WrongStep3(){}

    public MachineZ chooseMachine(MachineZ target1, MachineZ target2)
    {
        return target1;
    }

    public void onMachineRequest(Product product, MachineZ m1, MachineZ m2, MachineZ m3)
    {
        m1.executeJob(product);
        m2.executeJob(product);
        m3.executeJob(product);
        product.makeFinshed();
    }
    
}